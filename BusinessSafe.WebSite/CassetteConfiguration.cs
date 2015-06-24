using System.IO;
using Cassette;
using Cassette.Scripts;
using Cassette.Stylesheets;

namespace BusinessSafe.WebSite
{
    /// <summary>
    /// Configures the Cassette asset bundles for the web application.
    /// </summary>
    public class 
        CassetteBundleConfiguration : IConfiguration<BundleCollection>
    {
        public void Configure(BundleCollection bundles)
        {
            // JQUERY javascript files
            var jqueryFiles = new[]
                                       {
                                           "Scripts/Libraries/jquery-1.7.1.js",
                                           "Scripts/Libraries/jquery.validate.js",
                                           "Scripts/Libraries/jquery.validate.unobtrusive.js",
                                           "Scripts/Libraries/jquery-ui-1.8.18.js",
                                           "Scripts/Libraries/jquery.ui.selectmenu.js",
                                           "Scripts/Libraries/jquery.treeview.js",
                                           "Scripts/Libraries/jquery.jOrgChart.js",
                                           "Scripts/Libraries/jquery.tablesorter.js",
                                           "Scripts/Libraries/jquery.multisortable.js"
                                       };
            bundles.Add<ScriptBundle>("jquery", jqueryFiles);

            // BOOTSTRAP javascript files
            var bootstrapFiles = new[]
                                       {
                                           "Scripts/Libraries/bootstrap-tab.js",
                                           "Scripts/Libraries/bootstrap-tooltip.js",
                                           "Scripts/Libraries/bootstrap-dropdown.js",
                                           "Scripts/Libraries/bootstrap-popover.js",
                                           "Scripts/Libraries/bootstrap-button.js"
                                       };
            bundles.Add<ScriptBundle>("bootstrap", bootstrapFiles);

            // Microsoft javascript files
            var microsoftFiles = new[]
                                       {
                                           "Scripts/Libraries/MicrosoftAjax.debug.js",
                                           "Scripts/Libraries/MicrosoftAjax.js",
                                           "Scripts/Libraries/MicrosoftMvcAjax.debug.js",
                                           "Scripts/Libraries/MicrosoftMvcAjax.js",
                                           "Scripts/Libraries/MicrosoftMvcValidation.debug.js",
                                           "Scripts/Libraries/MicrosoftMvcValidation.js"
                                       };
            bundles.Add<ScriptBundle>("microsoft", microsoftFiles);


            // Business Safe Application Files
            var applicationFiles = new[]
                                       {
                                           "Scripts/libraries/json2.js",
                                           "Scripts/BusinessSafe/application/application.js",
                                           "Scripts/BusinessSafe/application/cookies.js",
                                           "Scripts/BusinessSafe/application/dropdownautocomplete.js",
                                           "Scripts/BusinessSafe/application/pbs.plugins.multiselectandsearch.js",
                                           "Scripts/BusinessSafe/application/utils.js",
                                           "Scripts/BusinessSafe/application/help.js",
                                       };
            bundles.Add<ScriptBundle>("application", applicationFiles);

            // Employees Files
            var employeesFiles = new[] { 
			                        "Scripts/BusinessSafe/employees-search.js", 
                                    "Scripts/BusinessSafe/employees-maintenance-emergencycontacts.js",
                                    "Scripts/BusinessSafe/employees-maintenance.js"
	                        };

            bundles.Add<ScriptBundle>("employees", employeesFiles);

            // Sites Files
            var sitesFiles = new[] { 
			                        "Scripts/BusinessSafe/site-structure.js"
	                        };

            bundles.Add<ScriptBundle>("sites", sitesFiles);

            // Users Files
            var usersFiles = new[] { 
			                        "Scripts/BusinessSafe/user-search.js",
                                    "Scripts/BusinessSafe/user-roles.js",
                                    "Scripts/BusinessSafe/user-permissions.js"
	                        };

            bundles.Add<ScriptBundle>("users", usersFiles);

            // Shared Risk Assessment Files
            var sharedRiskAssessmentFiles = new[] { 
                                "Scripts/BusinessSafe/company-defaults-add-non-employee-manager.js",
			                    "Scripts/BusinessSafe/businesssafe.riskassessment.summary.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.documents.js",
                                "Scripts/BusinessSafe/attach-documents.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.reviews.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.employees.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.nonemployees.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.summarytab.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.search.js"
	                    };

            bundles.Add<ScriptBundle>("sharedriskassessment", sharedRiskAssessmentFiles);

            // General Risk Assessment Files
            var generalRiskAssessmentFiles = new[] { 
			                    "Scripts/BusinessSafe/generalriskassessments.hazards.manager.js",
                                "Scripts/BusinessSafe/generalriskassessments.premisesinformation.js",
                                };

            bundles.Add<ScriptBundle>("generalriskassessment", generalRiskAssessmentFiles);

            // Task Files
            var taskFiles = new[] {
                                "Scripts/BusinessSafe/businesssafe.tasks.tablerow.parameter.extractor.js",
			                    "Scripts/BusinessSafe/employee-tasks.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.controlmeasures.descriptions.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.controlmeasures.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.furthercontrolmeasuretasks.bulkreassign.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.riskassessment.options.js",
                                "Scripts/BusinessSafe/businesssafe.riskassessment.tasks.grid.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.gridrow.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.reassign.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.view.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.remove.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.new.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.summarydetails.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.edit.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.nolongerrequired.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.acceptancetest.helper.js",
                                "Scripts/BusinessSafe/businesssafe.actionplan.reassign.task.js",
	                    };

            bundles.Add<ScriptBundle>("tasks", taskFiles);

            // Task List Files
            var taskListFiles = new[] {
                                "Scripts/BusinessSafe/businesssafe.tasks.tablerow.parameter.extractor.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.reassign.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.remove.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.print.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.view.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasks.complete.viewmodel.js",
                                "Scripts/BusinessSafe/businesssafe.tasklist.summary.js",
			                    "Scripts/BusinessSafe/employee-tasks.js",
                                "Scripts/BusinessSafe/attach-documents.js",
                                "Scripts/BusinessSafe/businesssafe.actionplan.reassign.task.js"
	                    };

            bundles.Add<ScriptBundle>("taskslist", taskListFiles);


            // Hazardous Substance Inventory Files
            var hazardousSubstanceInventoryFiles = new[] { 
                                "Scripts/BusinessSafe/businesssafe.hazardoussubstance.inventory.search.js",
                                "Scripts/BusinessSafe/businesssafe.hazardoussubstance.inventory.js",
                                "Scripts/BusinessSafe/businesssafe.hazardoussubstance.suppliers.js",
	                    };

            bundles.Add<ScriptBundle>("hazardoussubstanceinventory", hazardousSubstanceInventoryFiles);

            // Hazardous Substance Risk Assessment Files
            var hazardousSubstanceRiskAssessmentFiles = new[] { 
                            "Scripts/BusinessSafe/businesssafe.hazardoussubstance.description.js",
                            "Scripts/BusinessSafe/businesssafe.hazardoussubstance.assessment.js",
	                };

            bundles.Add<ScriptBundle>("hazardoussubstanceriskassessment", hazardousSubstanceRiskAssessmentFiles);

            // Personal Risk Assessment Files
            var personalRiskAssessmentFiles = new[] { 
                            "Scripts/BusinessSafe/businesssafe.personalriskassessment.hazards.js",
                            "Scripts/BusinessSafe/businesssafe.personalriskassessment.premisesinformation.js",
                            "Scripts/BusinessSafe/businesssafe.personalriskassessment.checklists.manager.js",
                            "Scripts/BusinessSafe/businesssafe.personalriskassessment.checklists.summary.js",
                            "Scripts/BusinessSafe/businesssafe.personalriskassessment.checklists.generator.js"
	                };

            bundles.Add<ScriptBundle>("personalriskassessment", personalRiskAssessmentFiles);

            // Fire Risk Assessment Files
            var fireRiskAssessmentFiles = new[] { 
                            "Scripts/BusinessSafe/businesssafe.fireriskassessment.hazards.js",
                            "Scripts/BusinessSafe/businesssafe.fireriskassessment.premisesinformation.js",
                            "Scripts/BusinessSafe/businesssafe.fireriskassessment.checklist.js",
                            "Scripts/BusinessSafe/businesssafe.fireriskassessment.significantfinding.js"
	                };

            bundles.Add<ScriptBundle>("fireriskassessment", fireRiskAssessmentFiles);


            // Company Default Files
            var companyDefaultsFiles = new[] { 
                            "Scripts/BusinessSafe/businesssafe.companydefaults.riskassessors.dommanipulation.js",
                            "Scripts/BusinessSafe/company-defaults-accident-record-distribution-list.js",
                            "Scripts/BusinessSafe/company-defaults-add-non-employee-manager.js",
                            "Scripts/BusinessSafe/company-defaults-edit-non-employee-manager.js",
                            "Scripts/BusinessSafe/company-defaults-add-risk-assessor.js",
                            "Scripts/BusinessSafe/company-defaults.js"
                          
	                };

            bundles.Add<ScriptBundle>("companydefaults", companyDefaultsFiles);

            // Company Detail Files
            var companyDetailFiles = new[] { 
                            "Scripts/BusinessSafe/company-details.js"
	                };

            bundles.Add<ScriptBundle>("companydetail", companyDetailFiles);

            // Added Documents Files
            var addedDocumentFiles = new[] { 
                            "Scripts/BusinessSafe/added-documents.js",
                            "Scripts/BusinessSafe/attach-documents.js"
	                };

            bundles.Add<ScriptBundle>("addeddocuments", addedDocumentFiles);

            // Business Safe System Document Files
            var businessSafeSystemDocumentFiles = new[] { 
                            "Scripts/BusinessSafe/businesssafesystem-documents.js",
            };

            bundles.Add<ScriptBundle>("businesssafesystemdocuments", businessSafeSystemDocumentFiles);

            // Reference Library Document Files
            var referenceLibraryDocumentFiles = new[] { 
                            "Scripts/BusinessSafe/document-library.js",
            };

            bundles.Add<ScriptBundle>("referencelibrarydocuments", referenceLibraryDocumentFiles);


            //Responsibilites Index
            var responsibilitiesIndexFiles = new[] {
                             "Scripts/BusinessSafe/businesssafe.responsibilities.search.js",
            };

            bundles.Add<ScriptBundle>("responsibilitiesIndex", responsibilitiesIndexFiles);

            var responsibilitiesFiles = new[] {
                            "Scripts/BusinessSafe/attach-documents.js",
                            "Scripts/BusinessSafe/businesssafe.responsibilities.edit.js",
                            "Scripts/BusinessSafe/businesssafe.responsibilities.task.js",
                            "Scripts/BusinessSafe/businesssafe.tasks.reassign.viewmodel.js",
                            "Scripts/BusinessSafe/businesssafe.tasks.view.viewmodel.js",
                            "Scripts/BusinessSafe/businesssafe.responsibilities.task.nolongerrequired.js",
                            "Scripts/BusinessSafe/businesssafe.actionplan.reassign.task.js"
            };

            bundles.Add<ScriptBundle>("responsibilities", responsibilitiesFiles);

            var responsibilitiesWizardFiles = new[] {
                             "Scripts/BusinessSafe/businesssafe.responsibilities.wizard.select-responsibilities.js",
                             "Scripts/BusinessSafe/businesssafe.responsibilities.wizard.assign-responsibilities.js",
                             "Scripts/BusinessSafe/businesssafe.responsibilities.wizard.generate-tasks.js",
            };

            bundles.Add<ScriptBundle>("responsibilitiesWizard", responsibilitiesWizardFiles);

            // Accident Reporting
            var accidentReportingFiles = new[] { 
                                "Scripts/BusinessSafe/businesssafe.accidentrerecord.injurydetails.js",
                                "Scripts/BusinessSafe/businesssafe.accidentrerecord.injuredperson.js",
                                "Scripts/BusinessSafe/businesssafe.accidentrecord.accidentdetails.js",
								"Scripts/BusinessSafe/businesssafe.accidentrecord.accidentsummary.js",
                                "Scripts/BusinessSafe/businesssafe.accidentrerecord.overview.js",
                                "Scripts/BusinessSafe/attach-documents.js"
	                    };
            bundles.Add<ScriptBundle>("accidentReporting", accidentReportingFiles);

            // Core CSS
            var bootstrapCssFiles = new[] { 
                            "Content/bootstrap.css",
                            "Content/smoothness/jquery-ui-1.8.18.custom.css",
                            "Content/themes/bso_v1.5/core.css",
                            "Content/themes/bso_v1.5/ipad.css"
            };

            bundles.Add<StylesheetBundle>("core", bootstrapCssFiles);

            // JQuery Treeview Css Files
            var jqueryTreeViewCssFiles = new[] { 
                            "Content/jquery.treeview.css"
            };

            bundles.Add<StylesheetBundle>("jquerytreeviewcss", jqueryTreeViewCssFiles);
            
            // Site Organisation Chart Css Files
            var siteOrganisationChartCssFiles = new[] { 
                            "Content/jquery.jOrgChart.css",
                            "Content/bootstrap.css",
                            "Content/themes/bso_v1.5/siteOrgChart.css"
            };

            bundles.Add<StylesheetBundle>("siteorganisationchartcss", siteOrganisationChartCssFiles);

            //Accident records index
            bundles.Add<ScriptBundle>("accidentReportsIndex", new[]
                                                                  {
                                                                      "Scripts/BusinessSafe/businesssafe.accidentrecords.search.js",
                                                                      "Scripts/BusinessSafe/businesssafe.accidentrecord.delete.js"
                                                                  });

            //Action Plan Index
            var actionPlanIndexFiles = new[] {
                             "Scripts/BusinessSafe/businesssafe.actionplan.search.js",
            };

            bundles.Add<ScriptBundle>("actionplanindex", actionPlanIndexFiles);

            //immediateRiskNotificationsActionsIndex
            var immediaterisknotificationactionsfiles = new[] {
                             "Scripts/BusinessSafe/attach-documents.js",
                             "Scripts/BusinessSafe/businesssafe.immediaterisknotificationsactions.search.js",
                             "Scripts/BusinessSafe/businesssafe.immediaterisknotificationsactions.action.assignedto.js"
            };

            bundles.Add<ScriptBundle>("immediaterisknotificationsactionsIndex", immediaterisknotificationactionsfiles);

            // SQL Report Files
            var sqlReportFiles = new[] { 			                        
                                    "Scripts/BusinessSafe/businesssafe.sqlreports.js"
	                        };

            bundles.Add<ScriptBundle>("sqlReports", sqlReportFiles);


            //Visit Request
            var visitRequestFiles = new[] {
                "Scripts/BusinessSafe/businesssafe.visitrequest.js"
            };

            bundles.Add<ScriptBundle>("visitRequest", visitRequestFiles);

            // Please read http://getcassette.net/documentation/configuration

            // This default configuration treats each file as a separate 'bundle'.
            // In production the content will be minified, but the files are not combined.
            // So you probably want to tweak these defaults!
            //bundles.AddPerIndividualFile<StylesheetBundle>("Content");
            //bundles.AddPerIndividualFile<ScriptBundle>("Scripts");

            // RecipientEmail combine files, try something like this instead:
            //   bundles.Add<StylesheetBundle>("Content");
            // In production mode, all of ~/Content will be combined into a single bundle.

            // If you want a bundle per folder, try this:
            //   bundles.AddPerSubDirectory<ScriptBundle>("Scripts");
            // Each immediate sub-directory of ~/Scripts will be combined into its own bundle.
            // This is useful when there are lots of scripts for different areas of the website.
        }
    }
}