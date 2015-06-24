require 'C:/PeninsulaRuby/geminstallations'
require 'C:/PeninsulaRuby/iissetup'
require 'C:/PeninsulaRuby/backup'
require 'albacore'
require 'albacore/albacoretask'
require 'open-uri'

srcpath = File.dirname(__FILE__).gsub("/","\\")

#BusinessSafe Main Site
deploypath = "c:\\BusinessSafe\\Deploy\\BusinessSafe"
BusinessSafeCIPublishPath ="c:/BusinessSafe/Deploy/PublishedCI/"
BusinessSafeCIPublishDirectory = BusinessSafeCIPublishPath.gsub("/", "\\")
BusinessSafeUATPublishPath = "c:/BusinessSafe/Deploy/PublishedUAT/"
BusinessSafeUATPublishDirectory = BusinessSafeUATPublishPath.gsub("/", "\\")

#BusinessSafe Checklists Site
checklistsDeployPath = "c:\\BusinessSafe\\Deploy\\BusinessSafeChecklists"
BusinessSafeChecklistsCIPublishPath ="c:/BusinessSafeChecklists/Deploy/PublishedCI/"
BusinessSafeChecklistsCIPublishDirectory = BusinessSafeCIPublishPath.gsub("/", "\\")
BusinessSafeChecklistsUATPublishPath = "c:/BusinessSafeChecklists/Deploy/PublishedUAT/"
BusinessSafeChecklistsUATPublishDirectory = BusinessSafeUATPublishPath.gsub("/", "\\")

messagehandlerdeploypath = "c:\\BusinessSafe\\Deploy\\BusinessSafe.MessageHandlers"
businesssafemessagehandlersemailsdeploypath = "c:\\BusinessSafe\\Deploy\\BusinessSafe.MessageHandlers.Emails"

task :default => :all
task :QuickResponse => [:test]
task :finetune =>      [:dbsetup, :deploy, :removeAndCreateCIPublishDir, :publishCI, :warmupsites, :performFineTuneTests]
task :allwithuitest => [:test, :dbsetup, :getVersion, :deploy, :removeAndCreateCIPublishDir, :publishCI, :removeAndCreateUATPublishDir, :publishUAT, :doTransforms, :copyTransform, :warmupsites, :acceptancetest]

desc "Warmup Sites"
task :warmupsites do
   print "Warm Up Sites \n"
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
    open("http://localhost:8078") do |f|
      print "warming businesssafe checklists \n"    
    end  
  rescue 
    # handle e
  end 
end

desc "Create IIS"
iissetup :create_iis do |d|
  d.path = "\"#{srcpath}\\BusinessSafe.WebSite\""
  d.apppoolname = "businesssafe"
  d.pipeline = "Integrated"
  d.site = "businesssafe"
  d.port = "8070"
  d.deploy_to = :website
end

desc "Build"
msbuild :build do |msb|
  msb.properties :configuration => :Debug
  msb.targets :Clean, :Build
  msb.solution = "BusinessSafe.sln"
end

desc "Test" 
nunit :test do |nunit|
	nunit.command = "ThirdParty/NUnit/nunit-console.exe"

	nunit.assemblies "BusinessSafe.WebSite.Test/bin/Debug/BusinessSafe.WebSite.Tests.dll", 
                   "BusinessSafe.Application.Tests/bin/Debug/BusinessSafe.Application.Tests.dll",
                   "BusinessSafe.Test.Infrastructure/bin/Debug/BusinessSafe.Test.Infrastructure.dll",
                   "BusinessSafe.Domain.Tests/bin/Debug/BusinessSafe.Domain.Tests.dll",
                   "BusinessSafe.Data.Tests/bin/Debug/BusinessSafe.Data.Tests.dll" ,
                   "BusinessSafe.Checklists.Test/bin/Debug/BusinessSafe.Checklists.Test.dll",    
                   "BusinessSafe.MessageHandlers.Emails.Test/bin/Debug/BusinessSafe.MessageHandlers.Emails.Test.dll",    
                   "BusinessSafe.MessageHandlers.Test/bin/Debug/BusinessSafe.MessageHandlers.Test.dll"   
	
	 nunit.options '/xml=UnitTestsResults.xml'

end

desc "Acceptance Tests"
nunit :acceptancetest   do |nunit|
  print "Acceptance Test\n"
  nunit.command = "ThirdParty/NUnit/nunit-console.exe"
  nunit.options '/nothread /exclude:finetune /xml=AcceptanceTestsResults.xml'
  nunit.assemblies "AcceptanceTests/bin/Debug/BusinessSafe.AcceptanceTests.dll"
end


desc "FineTune Acceptance Tests Only"
nunit :performFineTuneTests   do |nunit|
  nunit.command = "ThirdParty/NUnit/nunit-console.exe"
  nunit.options '/include:finetune /xml=FineTuneTestsResults.xml'
  nunit.assemblies "AcceptanceTests/bin/Debug/BusinessSafe.AcceptanceTests.dll"
end


desc "Create the BusinessSafe database known context"
   sqlcmd :create_businesssafe_database_known_context do |sql|
      script_CreateBusinessSafeDatabaseKnownContext = "SQLScripts\\BusinessSafeBaseData.sql"
      server = "UATSQL2"
      database = "master"
      sql.command = "sqlcmd.exe"
      sql.server = server
      sql.database = database            
      sql.scripts script_CreateBusinessSafeDatabaseKnownContext
end


desc "DBDeploy"
task :dbdeploy do
  system "#{"nant -buildfile:DBDeploy_Local.build -D:Target.BusinessSafe.SQLServer=UATSQL2 -D:Target.BusinessSafe.SQLServer.Filename=UATSQL2"}"
end 

task :dbsetup => [:create_initial_db,:dbdeploy,:create_businesssafe_database_known_context]

desc "Create the initial BusinessSafe database"
   sqlcmd :create_initial_db do |sql|
      script_CreateUser = "SQLScripts\\CREATE Security Login - intranetadmin.sql"
      script_dbsetup = "SQLScripts\\CREATE BusinessSafe database.PBSUATSQL3.sql"
      script_AddChangeLog = "SQLScripts\\CREATE ChangeLog table - BusinessSafe.sql"
      server = "UATSQL2"
      database = "master"
      sql.command = "sqlcmd.exe"
      sql.server = server
      sql.database = database            
      sql.scripts script_dbsetup, script_CreateUser, script_AddChangeLog      
end

desc "Deploy"

task :deploy => [:deployBusinessSafe, :deployBusinessSafeChecklists]

task :deployBusinessSafe do |cmd|
   system  "rd #{deploypath} /s /q"
   FileUtils.mkdir_p("#{deploypath}")
   system "xcopy #{srcpath}\\BusinessSafe.Website\ #{deploypath} /e /y"
   system "xcopy #{srcpath}\\packages\\WatiN.2.1.0\\lib\\net40 #{srcpath}\\AcceptanceTests\\bin\\Debug /e /y" 
   system "xcopy #{srcpath}\\packages\\MvcContrib.2.0.95.0\\lib #{srcpath}\\AcceptanceTests\\bin\\Debug /e /y" 
    
   system  "rd #{messagehandlerdeploypath} /s /q"
   FileUtils.mkdir_p("#{messagehandlerdeploypath}")
   system "xcopy #{srcpath}\\BusinessSafe.MessageHandlers\ #{messagehandlerdeploypath} /e /y"

   system  "rd #{businesssafemessagehandlersemailsdeploypath} /s /q"
   FileUtils.mkdir_p("#{businesssafemessagehandlersemailsdeploypath}")
   system "xcopy #{srcpath}\\BusinessSafe.MessageHandlers.Emails\ #{businesssafemessagehandlersemailsdeploypath} /e /y"
end

task :deployBusinessSafeChecklists do |cmd|
   system  "rd #{checklistsDeployPath} /s /q"
   FileUtils.mkdir_p("#{checklistsDeployPath}")
   system "xcopy #{srcpath}\\BusinessSafe.Checklists\ #{checklistsDeployPath} /e /y"
end

task :removeAndCreateCIPublishDir => [:removeAndCreateBusinessSafeCIPublishDir, :removeAndCreateBusinessSafeChecklistsCIPublishDir]

desc "Removes and recreates BusinessSafe CI publish directory"
task :removeAndCreateBusinessSafeCIPublishDir do
  print "Removing CI publish directory \n"
   system  "rd #{BusinessSafeCIPublishDirectory} /s /q"
  print "Creating CI publish directory \n"
   FileUtils.mkdir_p("#{BusinessSafeCIPublishDirectory}")
end

desc "Removes and recreates BusinessSafe Checklists CI publish directory"
task :removeAndCreateBusinessSafeChecklistsCIPublishDir do
  print "Removing Checklists CI publish directory \n"
   system  "rd #{BusinessSafeChecklistsCIPublishDirectory} /s /q"
  print "Creating Checklists CI publish directory \n"
   FileUtils.mkdir_p("#{BusinessSafeChecklistsCIPublishDirectory}")
end


task :publishCI => [:publishBusinessSafeCI, :publishBusinessSafeChecklistsCI]

desc "Publishes site in CI configuration"
msbuild :publishBusinessSafeCI do |msb|
  print "Publishes site in CI configuration \n"
  msb.properties :configuration => :Release, 
    :webprojectoutputdir => BusinessSafeCIPublishPath, 
    :outdir => BusinessSafeCIPublishPath + "bin/" 
  msb.targets :Clean, :Build
  msb.solution = "#{srcpath}\\BusinessSafe.WebSite\\BusinessSafe.WebSite.csproj"
end

desc "Publishes checklists in CI configuration"
msbuild :publishBusinessSafeChecklistsCI do |msb|
  print "Publishes checklists in CI configuration \n"
  msb.properties :configuration => :Release, 
    :webprojectoutputdir => BusinessSafeChecklistsCIPublishPath, 
    :outdir => BusinessSafeChecklistsCIPublishPath + "bin/" 
  msb.targets :Clean, :Build
  msb.solution = "#{srcpath}\\BusinessSafe.Checklists\\BusinessSafe.Checklists.csproj"
end

task :doTransforms => [:doTransformsWeb, :doTransformsMessageHandlers, :doTransformsEmailMessageHandlers, :doChecklistTransforms]

msbuild :doTransformsWeb do |msb|
  msb.targets :All
  msb.solution = "#{srcpath}\\BusinessSafe.WebSite\\Transformer.proj"
end

msbuild :doTransformsMessageHandlers do |msb|
  msb.targets :All
  msb.solution = "#{srcpath}\\BusinessSafe.MessageHandlers\\Transformer.proj"
end

msbuild :doTransformsEmailMessageHandlers do |msb|
  msb.targets :All
  msb.solution = "#{srcpath}\\BusinessSafe.MessageHandlers.Emails\\Transformer.proj"
end

msbuild :doChecklistTransforms do |msb|
  msb.targets :All
  msb.solution = "#{srcpath}\\BusinessSafe.Checklists\\Transformer.proj"
end


task :copyTransform do |msb|
  print "Copy Transform \n"
	system "xcopy #{srcpath}\\BusinessSafe.WebSite\\Web.CI.Transformed.Config #{BusinessSafeCIPublishDirectory}\\Web.config /e /y"
end
 
desc "setupmessagehandler"
task :setupmessagehandler do |cmd|
  system "net stop peninsulaOnline"       
   system "sc delete ""peninsulaOnline""" 
   system "#{messagehandlerdeploypath}\\bin\\Debug\\NServiceBus.Host.exe /install"
   system "net start peninsulaOnline"       
end 

desc "Setup BusinessSafe Emails Message Handler"
task :setupmessagehandlerbusinesssafeemailmessagehandler do |cmd|
  system "net stop businesssafeEmailsMessageHandlers"       
   system "sc delete ""businesssafeEmailsMessageHandlers""" 
   system "#{businesssafemessagehandlersemailsdeploypath}\\bin\\Debug\\NServiceBus.Host.exe /install /serviceName:""businesssafeEmailsMessageHandlers"""
   system "net start businesssafeEmailsMessageHandlers" 
end 


desc "Removes UAT publish directory"
task :removeAndCreateUATPublishDir do
  print "Removing UAT publish directory \n"
   system  "rd #{BusinessSafeUATPublishDirectory} /s /q"
  print "Creating UAT publish directory \n"
   FileUtils.mkdir_p("#{BusinessSafeUATPublishDirectory}")
end

desc "Publishes sites in UAT configuration"
task :publishUAT => [:publishBusinessSafeUAT, :publishBusinessSafeChecklistsUAT]

msbuild :publishBusinessSafeUAT do |msb|
  msb.properties :configuration => :Debug, :UseWPP_CopyWebApplication => true, :PipelineDependsOnBuild => false,
    :webprojectoutputdir => BusinessSafeUATPublishPath, 
    :outdir => BusinessSafeUATPublishPath + "bin/" 
  msb.targets :Clean, :Build
  msb.solution = "#{srcpath}\\BusinessSafe.WebSite\\BusinessSafe.WebSite.csproj"
end

msbuild :publishBusinessSafeChecklistsUAT do |msb|
  msb.properties :configuration => :Debug, :UseWPP_CopyWebApplication => true, :PipelineDependsOnBuild => false,
    :webprojectoutputdir => BusinessSafeChecklistsUATPublishPath, 
    :outdir => BusinessSafeChecklistsUATPublishPath + "bin/" 
  msb.targets :Clean, :Build
  msb.solution = "#{srcpath}\\BusinessSafe.Checklists\\BusinessSafe.Checklists.csproj"
end

desc "Put Version number into web.config"
task :getVersion do 
  begin    
    #Get Build Number
    print "retrieving build number \n"
    if ENV['BUILD_NUMBER'].nil?
      raise "Please set ENV['BUILD_NUMBER'] and re-run."
    end
    BuildNumber = ENV['BUILD_NUMBER']
    print "BUILD_NUMBER: " + BuildNumber + " \n"

    #Get Time
    time = String.try_convert(Time.now.strftime("%d.%m.%Y"))

    #Write to web.config
    webConfigPath = "#{srcpath}\\BusinessSafe.WebSite"
    file = File.open "#{webConfigPath}\\web.config", "r" 
    xml=Nokogiri::XML   file
    xml.xpath('//appSettings/add').each do |node|
      vernode = node.xpath('@key')     
      if vernode.text=='Version'
        node['value'] = time + "."+ BuildNumber
        print   node['value']
      end      
    end
    File.open("#{webConfigPath}\\web.config", 'w') {|f| f.puts xml.to_xml }
  end
end