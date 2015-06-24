require 'C:/PeninsulaRuby/geminstallations'
require 'C:/PeninsulaRuby/iissetup'
require 'albacore'
require 'colorize'
require 'win32console'
require 'albacore/albacoretask'
require 'open-uri'
srcpath = File.dirname(__FILE__).gsub("/","\\")

task :default => :all
task :push => [:up , :add,:build,:jslint,:test,:acceptancetest]
task :all => [:up,:dbsetup,:build,:create_iis,:test,:acceptancetest]

desc "Create IIS"
iissetup :create_iis do |d|
  d.path = "\"#{srcpath}\\BusinessSafe.WebSite\""
  d.apppoolname = "businesssafe"
  d.pipeline = "Integrated"
  d.site = "businesssafe"
  d.port = "8070"
  d.deploy_to = :website
end

desc "Warmup Sites"
task :warmupsites do
  open("http://localhost:8070") do |f|
    print "warming business safe \n"
  end
  open("http://localhost:8072") do |f|
    print "warming client data services \n"    
  end
  begin
    open("http://localhost:8076") do |f|
      print "warming peninsula web \n"    
    end
    open("http://localhost:8074") do |f|
      print "warming peninsula maintenance \n"    
    end  
    open("http://localhost:8070/checklists") do |f|
      print "warming checklists \n"    
    end  
  rescue 
    # handle e
  end 
end

desc "Build"
msbuild :build do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Build
  msb.solution = "BusinessSafe.sln"
end

desc "All Tests"
task :alltests => [:test,:acceptancetest]

desc "Test" 
nunit :test do |nunit|
	nunit.command = "ThirdParty/NUnit/nunit-console.exe"
  nunit.options '/nothread /exclude:finetune'
	nunit.assemblies "BusinessSafe.WebSite.Test/bin/Debug/BusinessSafe.WebSite.Tests.dll", 
                   "BusinessSafe.Application.Tests/bin/Debug/BusinessSafe.Application.Tests.dll",
                   "BusinessSafe.Test.Infrastructure/bin/Debug/BusinessSafe.Test.Infrastructure.dll",
                   "BusinessSafe.Domain.Tests/bin/Debug/BusinessSafe.Domain.Tests.dll",
                   "BusinessSafe.Data.Tests/bin/Debug/BusinessSafe.Data.Tests.dll",
                   "BusinessSafe.Data.Tests/bin/Debug/BusinessSafe.Data.Tests.dll"
                   
end

desc "Acceptance Tests"
nunit :acceptancetest do |nunit|
  nunit.command = "ThirdParty/NUnit/nunit-console.exe"
  nunit.options '/nothread'
  nunit.assemblies "AcceptanceTests/bin/Debug/BusinessSafe.AcceptanceTests.dll"
end

desc "Create the BusinessSafe database known context"
   sqlcmd :create_businesssafe_database_known_context do |sql|
      script_CreateBusinessSafeDatabaseKnownContext = "SQLScripts\\BusinessSafeBaseData.sql"
      server = "(local)\\sql2008r2"
      database = "master"
      sql.command = "sqlcmd.exe"
      sql.server = server
      sql.database = database            
      sql.scripts script_CreateBusinessSafeDatabaseKnownContext
end


desc "Run NCover Console code coverage"
ncoverconsole :coverage do |ncc|
  ncc.command = "C:/Program Files/NCover/NCover.Console.exe"
  ncc.output :xml => "TestResults/test-coverage.xml"  
  ncc.exclude_attributes("BusinessSafe.Infrastructure.Attributes.CoverageExcludeAttribute")
  ncc.exclude_assemblies("Iesi.Collections.dll","NHibernate.dll","NHibernate.ByteCode.Castle",".*Tests.dll")

  nunit = NUnitTestRunner.new("ThirdParty/NUnit/nunit-console.exe")
  nunit.options '/noshadow'
  
  nunit.assemblies "BusinessSafe.WebSite.Test/bin/Debug/BusinessSafe.WebSite.Tests.dll", 
                   "BusinessSafe.Application.Tests/bin/Debug/BusinessSafe.Application.Tests.dll",
                   "BusinessSafe.Test.Infrastructure/bin/Debug/BusinessSafe.Test.Infrastructure.dll",
                   "BusinessSafe.Domain.Tests/bin/Debug/BusinessSafe.Domain.Tests.dll",
                   "BusinessSafe.Data.Tests/bin/Debug/BusinessSafe.Data.Tests.dll"                   

  ncc.testrunner = nunit
end


desc "Run a sample NCover Report to check code coverage"
ncoverreport :ncoverreport do |ncr|
  print "test"
  @xml_coverage = "TestResults/test-coverage.xml"
  
  ncr.command = "C:/Program Files/NCover/NCover.Reporting.exe"
  ncr.coverage_files @xml_coverage
  
  fullcoveragereport = NCover::FullCoverageReport.new
  fullcoveragereport.output_path = "TestResults"
  ncr.reports fullcoveragereport
  
  ncr.required_coverage(
    NCover::BranchCoverage.new(:minimum => 10),
    NCover::CyclomaticComplexity.new(:maximum => 1)
  )
end


desc "Pull and Up"
task :up do      
     begin      
      dir =  Dir[".hg"]
      print "Dir : #{dir}\n".green
      
      if dir.nil?
        print 'Local copy of repository does not exists.'.red        
      else        
        system "#{"hg pull -u"}"
      end       
       
     rescue => e
       print "Failed because: #{e}".red
     end
end

desc "Hg Add"
task :add do      
     begin
      dir =  Dir[".hg"]
      print "Dir : #{dir}\n".green
      
      if dir.nil?
        print 'Local copy of repository does not exists.'.red        
      else        
        system "#{"hg add"}"
      end       
       
     rescue => e
       print "Failed because: #{e}".red
     end
end

desc "DBDeploy"
task :dbdeploy do
  print "Starting DB Deploy...\n".green
  system "#{"nant -buildfile:DBDeploy_Local.build "}"  
end 

task :dbsetup => [:create_initial_db,:dbdeploy,:create_businesssafe_database_known_context]

desc "Create the initial BusinessSafe database"
   sqlcmd :create_initial_db do |sql|
      script_CreateUser = "SQLScripts\\CREATE Security Login - intranetadmin.sql"
      script_CreateBusinessSafe = "SQLScripts\\CREATE BusinessSafe database.localhost_sql2008R2.sql"
      script_AddChangeLog = "SQLScripts\\CREATE ChangeLog table - BusinessSafe.sql"
      server = "(local)\\sql2008r2"
      database = "master"
      sql.command = "sqlcmd.exe"
      sql.server = server
      sql.database = database            
      sql.scripts script_CreateBusinessSafe, script_CreateUser, script_AddChangeLog      
      print "BusinessSafe Database has been created successfully!\n".green
end

desc "Check the JavaScript source with JSLint - exit with status 1 if any of the files fail."
task :jslint do
  failed_files = []
 
  Dir['BusinessSafe.WebSite/Scripts/BusinessSafe/**/*.js'].each do |fname|    
    cmd = "ThirdParty/jsl-0.3.0/jsl.exe conf ThirdParty/jsl-0.3.0/jsl.default.conf process #{fname} "
    results = %x{#{cmd}}

   
    if String.try_convert(results).include? "0 error(s), 0 warning(s)" 
      puts "\n#{fname} is valid!".green
    else
      puts "#{fname} is not valid.".red
      puts results.white
      failed_files << fname
    end
   
  end
  if failed_files.size > 0
    exit 1
  end
end