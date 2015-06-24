begin
   Gem::Specification.find_by_name('albacore')
rescue Gem::LoadError
   Gem.available?('albacore')
    system "#{"gem install 'albacore'"}"
end

begin
   Gem::Specification.find_by_name('nokogiri')
rescue Gem::LoadError
   Gem.available?('nokogiri')
   system "#{"gem install 'nokogiri'"}"
end

require 'albacore'
require 'albacore/albacoretask'
require 'nokogiri'

class IISSetup
  include Albacore::Task

  attr_hash :override_with
  attr_accessor :deploy_to, :apppoolname, :pipeline, :site, :path, :apppath, :port

  def initialize()
    super()
    override_with = {}
    deploy_to = :website
  end

  def execute
    update_attributes override_with unless override_with.nil?
    setup_apppool
    deploy_site if deploy_to == :website
    deploy_app if deploy_to == :app
  end

  private
  def setup_apppool
    xml = Nokogiri::XML(`#{ENV['windir']}/system32/inetsrv/appcmd list apppool -apppool.name:#{apppoolname} /xml`).xpath("//APPPOOL")

    if xml.empty?
      puts "Setting up App Pool: #{apppoolname}"
      cmd = Exec.new
      cmd.command = "#{ENV['windir']}/system32/inetsrv/appcmd"
      cmd.parameters "add apppool -name:#{apppoolname} -managedPipelineMode:#{pipeline} -enable32BitAppOnWin64:true -managedRuntimeVersion:v4.0 "
      cmd.execute
    else
      puts "App Pool already exists: #{apppoolname}"
    end
  end

  def deploy_site
    xml = Nokogiri::XML(`#{ENV['windir']}/system32/inetsrv/appcmd list site -site.name:#{site} /xml`).xpath("//SITE")

    if xml.empty?
      puts "Deploying to site: #{site}"
      cmd = Exec.new
      cmd.command = "#{ENV['windir']}/system32/inetsrv/appcmd"
      cmd.parameters "add site /name:#{site} /physicalPath:#{path} /bindings:http/*:#{port}:"
      cmd.execute
      cmd.parameters "set site #{site} -applicationDefaults.applicationPool:#{apppoolname} "
      cmd.execute
    else
      puts "Website already exists: #{site}"
    end
  end

  def deploy_app
    xml = Nokogiri::XML(`#{ENV['windir']}/system32/inetsrv/appcmd list app -app.name:#{site}#{apppath} /xml`).xpath("//APP/@path")

    if xml.empty? or xml.to_s != apppath
      puts "Deploying to application: #{site}#{apppath}"
      cmd = Exec.new
      cmd.command = "#{ENV['windir']}/system32/inetsrv/appcmd"
      cmd.parameters "add app /site.name:#{site} /path:#{apppath} /physicalPath:#{path} /applicationPool:#{apppoolname} "
      cmd.execute
    else
      puts "Application already exists: #{site}#{apppath}"
    end
  end
end