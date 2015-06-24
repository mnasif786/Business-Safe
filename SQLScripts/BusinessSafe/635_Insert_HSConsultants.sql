INSERT INTO dbo.SafeCheckConsultant
        ( Id
        ,Username
        ,Forename
        ,Surname
        ,Email
        ,PercentageOfChecklistsToSendToQualityControl
        ,deleted
        )
SELECT a.*,0
FROM (
SELECT '2cdc3ae4-31ae-4e7d-a976-9ecc85a80dfe' AS Id,'Alan.Gordon' AS Username,'Alan' AS Forename,'Gordon' AS surname,'alan.gordon@peninsula-uk.com' AS email, 20 AS Percentage
UNION SELECT '2ece7327-49ac-4709-9096-22adf200206a','Alan.Kelly','Alan','Kelly','Alan.Kelly@peninsula-uk.com', 20
UNION SELECT '54ff4a1a-b050-4b77-aa10-3f5f4fda50ce','Alan.Mackrell','Alan','Mackrell','Alan.Mackrell@peninsula-uk.com', 20
UNION SELECT '89fb5ac4-9c71-4f05-94e2-7e883ffd20df','Allan.Brizland','Allan','Brizland','Allan.Brizland@peninsula-uk.com', 20
UNION SELECT 'c58d09ba-713d-4444-9b6e-85f31c79e048','Angela.Loboda','Angela','Loboda','Angela.Loboda@peninsula-uk.com', 20
UNION SELECT 'd4530167-d717-4624-ad7f-63f9958ead87','Anthony.Sak','Anthony','Sak','Anthony.Sak@peninsula-uk.com', 20
UNION SELECT 'ad568eb5-436f-45af-970c-c579f9b9bfee','Barry.Steging','Barry','Steging','Barry.Steging@peninsula-uk.com', 20
UNION SELECT 'cbe308d2-75ec-4595-9a33-330ae0b39fd1','Bill.Anderson','Bill','Anderson','Bill.Anderson@peninsula-uk.com', 20
UNION SELECT '9d4b1dab-2863-452d-ad10-ea5c0cf4cc89','Bob.Standfast','Bob','Standfast','Bob.Standfast@peninsula-uk.com', 20
UNION SELECT '110f9cfb-1ef8-439c-b857-d970b5753ca6','Brendan.Hanratty','Brendan','Hanratty','Brendan.Hanratty@peninsula-ie.com', 20
UNION SELECT '6d02f4f4-1a62-492d-8f30-6b4d614bcef1','Brett.Rolls','Brett','Rolls','Brett.Rolls@peninsula-uk.com', 20
UNION SELECT '770bcc8d-a410-4259-ad9f-7c9d046f61e0','Brian.Lewis','Brian','Lewis','Brian.Lewis@peninsula-uk.com', 20
UNION SELECT 'e3060769-2221-4032-9661-43156da0f171','Bridgeen.Catterson','Bridgeen','Catterson','Bridgeen.Catterson@peninsula-ni.com', 20
UNION SELECT '7c0d6130-fcd4-4fe6-91d6-910ccd05108c','Caroline.McMullen','Caroline','McMullen','Caroline.McMullen@peninsula-uk.com', 20
UNION SELECT 'd2eff3f0-4b85-4c15-9ecc-8c64cac9caf2','Catherine.OBrien','Catherine','OBrien','Katy.OBrien@peninsula-uk.com', 20
UNION SELECT '766a95b0-afb1-4888-8a9a-16ad04c38697','Charleen.Riley','Charleen','Riley','Charleen.Riley@peninsula-uk.com', 20
UNION SELECT 'ee9c07ff-a13e-4bdc-b634-8ef1c56dfe64','Charlotte.Bennett','Charlotte','Bennett','Charlotte.Bennett@peninsula-uk.com', 20
UNION SELECT '96fd0517-3523-4610-973f-ff9ac348a385','Cole.Porter','Cole','Porter','Cole.Porter@peninsula-uk.com', 20
UNION SELECT 'f719125f-fd4f-485c-b4ba-f6238317bbcf','Colin.Beall','Colin','Beall','Colin.Beall@peninsula-uk.com', 20
UNION SELECT 'fe24ea5e-4ce1-40b5-93d2-492b12eaffd0','Colin.Martin','Colin','Martin','Colin.Martin@peninsula-uk.com', 20
UNION SELECT '4564d21a-be09-4b97-b00d-088bb71cd05b','Dave.Howard','Dave','Howard','Dave.Howard@peninsula-uk.com', 20
UNION SELECT '2c470cbf-d3c1-41df-99ee-6e5330703d0b','David.Langford','David','Langford','David.Langford@peninsula-uk.com', 20
UNION SELECT '2928b592-28cb-4db4-8859-8dd8ef72821a','David.Race','David','Race','David.Race@peninsula-uk.com', 20
UNION SELECT 'c42f6ba8-be37-4f76-b1e8-e21c14d04ba1','David.Smith','David','Smith','David.Smith@peninsula-uk.com', 20
UNION SELECT '0b55acf3-8287-498f-8173-9cde5f11bf04','Diana.Howlett','Diana','Howlett','Diana.Howlett@peninsula-uk.com', 20
UNION SELECT 'ea58cf8d-ab8f-4e8a-ad74-08ad3dec89f9','Elizabeth.Hilton','Elizabeth','Hilton','Elizabeth.Hilton@peninsula-uk.com', 20
UNION SELECT 'cf1704a5-4960-4efe-88d3-e8d31bd0054d','Emma.Yorke','Emma','Yorke','Emma.Yorke@peninsula-uk.com', 20
UNION SELECT '12758d65-9ab5-4fb3-b7d9-564cba07d48d','Eva.Toth','Eva','Toth','Eva.Toth@peninsula-uk.com', 20
UNION SELECT '19b71c77-7d49-4583-a1f7-da933cf0c665','Gareth.Byrne','Gareth','Byrne','Gareth.Byrne@peninsula-uk.com', 20
UNION SELECT '7479a305-c8e7-4f41-94e9-7ebac94a02db','Garry.Mcfadden','Garry','McFadden','Garry.Mcfadden@peninsula-uk.com', 20
UNION SELECT '83262850-4c45-484c-9086-e57c4fc53c26','Gemma.Kelly','Gemma','Kelly','Gemma.Kelly@peninsula-uk.com', 20
UNION SELECT '32c6a698-adfd-44c0-a410-d5708edf1632','George.Baird','George','Baird','George.Baird@peninsula-uk.com', 20
UNION SELECT '72a43c26-5bac-4fd9-a491-a2e68177f057','George.Cooper','George','Cooper','George.Cooper@peninsula-uk.com', 20
UNION SELECT '663560c3-8f90-43db-a21a-a368a52902da','Gordon.Bannatyne','Gordon','Bannatyne','Gordon.Bannatyne@peninsula-uk.com', 20
UNION SELECT '186df803-4e1a-4314-8c6d-0ec25a7b0766','Helen.Armitt','Helen','Armitt','Helen.Armitt@peninsula-uk.com', 20
UNION SELECT '4c468c34-663f-4bb9-9133-d8e96800cd2f','Helen.Hollins-West','Helen','Hollins-West','Helen.Hollins-West@peninsula-uk.com', 20
UNION SELECT 'a6338050-4a06-47bd-af8a-603bf216779f','Holly.Smith','Holly','Smith','Holly.Smith@peninsula-uk.com', 20
UNION SELECT 'fcda1404-1b4f-4130-86b7-94538ed1df14','Howard.Cole','Howard','Cole','Howard.Cole@peninsula-uk.com', 20
UNION SELECT '916e769a-5616-4c43-9be6-3c3244792556','Ian.Bloxsome','Ian','Bloxsome','Ian.Bloxsome@peninsula-uk.com', 20
UNION SELECT 'da57e4a2-a789-42a5-9007-251c9f429720','Ian.Cognito','Ian','Cognito','Ian.Cognito@peninsula-uk.com', 20
UNION SELECT '32e30cf1-1819-4bcc-8b47-3a0269daf2f0','Ian.Goodson','Ian','Goodson','Ian.Goodson@peninsula-uk.com', 20
UNION SELECT 'ead17304-68bf-487c-bbfe-9ad971f43be7','Ian.Smith','Ian','Smith','Ian.Smith@peninsula-uk.com', 20
UNION SELECT 'b1fc4022-c75b-4568-a4fc-4825c78bafa0','James.Barrie','James','Barrie','James.Barrie@peninsula-uk.com', 20
UNION SELECT '90d62838-6ea5-4c06-89f5-0dc3405fdeab','James.Scheu','James','Scheu','James.Scheu@peninsula-uk.com', 20
UNION SELECT '9f2bc1ec-00b9-47b0-87e5-407f7e9aa4c7','Jamie.Fogg','Jamie','Fogg','Jamie.Fogg@peninsula-uk.com', 20
UNION SELECT '85126e78-58e5-4595-880f-9c356afc14bf','Jane.Ball','Jane','Ball','Jane.Ball@peninsula-uk.com', 20
UNION SELECT '9ce1756e-de50-49d2-9a03-bb60811f07d2','Jane.Neil','Jane','Neil','Jane.Neil@Peninsula-uk.com', 20
UNION SELECT '67130bb8-3487-4d5e-afba-89ff29b33a8b','Jenny.Trish','Jenny','Trish','Jenny.Trish@peninsula-uk.com', 20
UNION SELECT '015a7b1a-7e15-490a-bff3-23f069b35812','Jessica.Taylor','Jessica','Taylor','Jessica.Taylor@peninsula-uk.com', 20
UNION SELECT '50b8cf99-2925-4714-bcf3-1a42a09394f4','Joanna.Seechim','Joanna','Seechim','Joanna.Seechim@peninsula-uk.com', 20
UNION SELECT 'aa73cc30-941a-4cf8-b301-702cca649ffc','Joanne.Byrne','Joanne','Byrne','Joanne.Byrne@peninsula-ie.com', 20
UNION SELECT 'e1c405a6-fdd8-4d7e-bbe8-924e125e4bce','John.Mvududu','John','Mvududu','John.Mvududu@peninsula-uk.com', 20
UNION SELECT 'beae56b0-dfe8-4ffa-ae25-500bdf3cd521','Jonathan.Inglis','Jonathan','Inglis','Jonathan.Inglis@peninsula-uk.com', 20
UNION SELECT 'b21939b8-2eed-4da9-82c7-3d3734503bb4','Kayleigh.Brown','Kayleigh','Brown','Kayleigh.Brown@peninsula-uk.com', 20
UNION SELECT 'b5ece733-a1ab-4dd8-9ace-fbc816517212','Kevin.Munday','Kevin','Munday','Kevin.Munday@peninsula-uk.com', 20
UNION SELECT '89620337-3b46-4348-8be9-18feea27b693','Kim.Birchall','Kim','Birchall','Kim.Birchall@peninsula-uk.com', 20
UNION SELECT '495d07f7-bcfa-40fd-9fe1-e64cbee50dac','Lee.Craig','Lee','Craig','Lee.Craig@peninsula-uk.com', 20
UNION SELECT '08d5b07c-cf38-419f-9b0d-330c1a4351c0','Lynne.Symes','Lynne','Symes','Lynne.Symes@peninsula-uk.com', 20
UNION SELECT '9d78f69c-37d8-49c1-a504-bccce312e4c6','Malcolm.Briggs','Malcolm','Briggs','Malcolm.Briggs@peninsula-uk.com', 20
UNION SELECT '3f390715-06eb-4e22-b307-8f5bb43a7cef','Margaret.Connelly','Margaret','Connelly','Margaret.Connelly@peninsula-ie.com', 20
UNION SELECT 'e6d215b3-72e2-49d2-8a9d-bdb01774a04f','Maria.Cunningham','Maria','Cunningham','Maria.Cunningham@peninsula-uk.com', 20
UNION SELECT '62119013-1057-494d-9cdc-9b3133094b0f','Mark.Proudley','Mark','Proudley','Mark.Proudley@peninsula-uk.com', 20
UNION SELECT 'f19098d2-358f-472c-89a2-ca5ff846627b','Martin.Stretton','Martin','Stretton','martin.stretton@peninsula-uk.com', 20
UNION SELECT '0da6d1cd-4261-4bb0-8794-0fdc25a2780b','Martin.Tagg','Martin','Tagg','Martin.Tagg@peninsula-uk.com', 20
UNION SELECT '5d223a1f-4d45-42f0-86ba-13febb2e24fd','Mehreen.Ahmad','Mehreen','Ahmad','Mehreen.Ahmad@peninsula-uk.com', 20
UNION SELECT '55502f03-ddf3-4e6c-a625-966c0bb84a2e','Micah.Webb','Micah','Webb','Micah.Webb@peninsula-uk.com', 20
UNION SELECT 'd990ab74-17cd-4bea-a8ca-4cd35d2367eb','Michael.Carter','Michael','Carter','Michael.Carter@peninsula-uk.com', 20
UNION SELECT '36192883-7b94-4f99-ad65-fa480b117367','Michael.Moran','Michael','Moran','Michael.Moran@peninsula-uk.com', 20
UNION SELECT '3c52093b-97dd-4f6a-886b-cf0d8f518e0e','Michael.Thomson','Michael','Thomson','Michael.Thomson@peninsula-uk.com', 20
UNION SELECT '5d15ffd9-0458-4824-9d5e-d414bfcea717','Michelle.Berry','Michelle','Berry','Michelle.Berry@peninsula-uk.com', 20
UNION SELECT '1abccac6-e16e-4f60-8ee2-ddf876d90819','Natalie.Fitzsimons','Natalie','Fitzsimons','Natalie.Fitzsimons@peninsula-uk.com', 20
UNION SELECT 'c7aecd75-4178-425c-b480-2e35105a2030','Neil.Pinnock','Neil','Pinnock','neil.pinnock@peninsula-uk.com', 20
UNION SELECT '6b8d4d73-7ce1-403f-b644-5c722dbf1e93','Oliver.Stockdale','Oliver','Stockdale','Oliver.Stockdale@peninsula-uk.com', 20
UNION SELECT '51a1439a-c55b-40b2-a2c0-adacfbf72eb0','Paul.Camwell','Paul','Camwell','Paul.Camwell@peninsula-uk.com', 20
UNION SELECT 'ec5e748f-4a42-46ed-8a6d-57b2ce643426','Paul.Jackson','Paul','Jackson','Paul.Jackson@peninsula-uk.com', 20
UNION SELECT 'c7b7d10e-f139-40fc-9ec1-eb396ef94c9b','Paul.Moore','Paul','Moore','Paul.Moore@peninsula-ni.com', 20
UNION SELECT '5071d055-48bf-4ad1-bcf0-639d0949e5a8','Paul.Starkey','Paul','Starkey','Paul.Starkey@peninsula-uk.com', 20
UNION SELECT '171c11e3-d253-4b77-8c49-7f012a54743f','Paula.Meldrum','Paula','Meldrum','paula.meldrum@peninsula-uk.com', 20
UNION SELECT '0506e46a-e7ba-49c5-ae1c-965fcc53cbfb','Peter.Blackwood','Peter','Blackwood','Peter.Blackwood@peninsula-uk.com', 20
UNION SELECT '49421c15-b479-4958-be1a-2e38756689b6','Peter.deSalis','Peter','deSalis','Peter.deSalis@peninsula-uk.com', 20
UNION SELECT '50c5070d-7a53-4661-a6bb-5b98b4988b9a','Pippa.Dunkerley','Pippa','Dunkerley','Pippa.Dunkerley@peninsula-uk.com', 20
UNION SELECT 'ade449ab-68e5-46d0-b09a-8eaa938f42b8','Rachel.Donnelly','Rachel','Donnelly','Rachel.Donnelly@peninsula-uk.com', 20
UNION SELECT '10f48da0-2eb7-46cf-b4bc-05f402caa54d','Rebecca.Connolly','Rebecca','Connolly','Rebecca.Connolly@peninsula-uk.com', 20
UNION SELECT '980d64fe-1fe2-482e-8948-714686c4c8d1','Rebecca.Maslin','Rebecca','Maslin','Rebecca.Maslin@peninsula-uk.com', 20
UNION SELECT 'dc41c4d7-b629-4909-9501-614ea4a1ca27','Rebekah.Collins','Rebekah','Collins','Rebekah.Collins@peninsula-uk.com', 20
UNION SELECT 'f4d705a8-d4b4-41ea-a89d-3500b4c8c658','Richard.Bourke','Richard','Bourke','Richard.Bourke@peninsula-ie.com', 20
UNION SELECT '1322c28d-6be9-4eac-b992-182ed9ce1cf8','Richard.Garner','Richard','Garner','Richard.Garner@peninsula-uk.com', 20
UNION SELECT '593d6040-25a6-4001-9428-cad52a7f904a','Richard.Matthews','Richard','Matthews','Richard.Matthews@peninsula-uk.com', 20
UNION SELECT '14aeb0d4-0507-470b-aaa8-bd1942b76a63','Rob.Parr','Rob','Parr','Rob.Parr@peninsula-uk.com', 20
UNION SELECT '2f73a1e8-d6ba-4d12-a3b4-7ee460e652ad','Robin.Harrison','Robin','Harrison','robin.harrison@peninsula-uk.com', 20
UNION SELECT '527edfd0-4eee-4f88-9898-500d909b8c69','Ron.Cooper','Ron','Cooper','ron.cooper@peninsula-uk.com', 20
UNION SELECT '93597106-4817-49b4-9e79-82d5188df68b','Sarah.Walsh','Sarah','Walsh','Sarah.Walsh@peninsula-uk.com', 20
UNION SELECT '990cb9ca-9ec4-4f32-af19-a67489cab946','sean.murphy','Sean','Murphy','sean.murphy@peninsula-ie.com', 20
UNION SELECT '4a50f230-5ef6-431b-a29b-161fed195625','Simon.Hopes','Simon','Hopes','Simon.Hopes@peninsula-uk.com', 20
UNION SELECT '8dc05a29-583d-4c28-9b7e-4dfb616fb26a','Sinead.Lewis','Sinead','Lewis','Sinead.Lewis@peninsula-uk.com', 20
UNION SELECT 'd4ac15b4-9a2c-445b-916c-c21e1cc5afea','Sonia.Morante','Sonia','Morante','Sonia.Morante@peninsula-uk.com', 20
UNION SELECT '95888e89-fa9d-41c6-ac0f-e6a46a786818','Sophia.Maduka','Sophia','Maduka','Sophia.Maduka@peninsula-uk.com', 20
UNION SELECT '4ba8107c-4f57-4721-bd45-e9c11bb2fe80','Stephanie.Byrne','Stephanie','Byrne','Stephanie.Byrne@peninsula-ie.com', 20
UNION SELECT '2e14e947-1884-47f3-98e9-a60102bdfe6a','Stephen.Galley','Stephen','Galley','Stephen.Galley@peninsula-uk.com', 20
UNION SELECT 'e712281b-67ee-498c-8d7e-fcc8ce74079c','Stephen.Green','Stephen','Green','Stephen.Green@peninsula-uk.com', 20
UNION SELECT '71c1fe4b-c81d-4f02-905b-ad5d8c563f0e','Stephen.Hall','Stephen','Hall','Stephen.Hall@peninsula-uk.com', 20
UNION SELECT 'd1bed9fd-5d1f-4cef-a801-462fe88dd372','Stephen.Hrolfe','Stephen','Hrolfe','Stephen.Hrolfe@peninsula-uk.com', 20
UNION SELECT 'a032a5d7-13de-45b1-a607-4cf4ddd5d441','Tanya.Cocks','Tanya','Cocks','Tanya.Cocks@peninsula-uk.com', 20
UNION SELECT '8aa08416-1475-4503-9cd3-f4c6e9981d74','Theresa.Blake','Theresa','Blake','Theresa.Blake@peninsula-uk.com', 20
UNION SELECT 'c635747b-dc13-4d84-8abf-c9d0fe3262fa','Tony.Carden','Tony','Carden','Tony.Carden@peninsula-uk.com', 20
UNION SELECT 'a24a0eca-1031-413c-a69f-dd93f3815bc4','Tony.Reid','Tony','Reid','Tony.Reid@peninsula-uk.com', 20
UNION SELECT '1753c5c2-c965-4bd9-acf3-01f17d450514','Tristan.Grace','Tristan','Grace','Tristan.Grace@peninsula-uk.com', 20
UNION SELECT '1ab0b656-0233-4094-bc76-13e3cef9801a','yvette.Corfield','Yvette','Corfield','Yvette.Corfield@peninsula-uk.com', 20
	) a LEFT JOIN SafecheckConsultant c ON a.username = c.username
WHERE c.id IS null