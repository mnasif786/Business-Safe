require 'C:/PeninsulaRuby/geminstallations'
require 'C:/PeninsulaRuby/iissetup'
require 'albacore'
require 'colorize'
require 'win32console'
require 'albacore/albacoretask'

srcpath = File.dirname(__FILE__).gsub("/","\\")

task :default => :create_iis

iissetup :create_iis do |d|
  d.path = "\"#{srcpath}\\BusinessSafe.WCF\""
  d.apppoolname = "businesssafewcf"
  d.pipeline = "Integrated"
  d.site = "BusinessSafe.WCF"
  d.port = "8103"
  d.deploy_to = :website
end