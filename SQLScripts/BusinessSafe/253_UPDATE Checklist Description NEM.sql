USE [BusinessSafe]

UPDATE [dbo].[Checklist]
SET [Description] = '<b>Important Notes:</b><br/>' +
'<ul>' +
'<li>This assessment should be completed by both the pregnant woman and a supervisor, a copy should  be giver to the person subject to this assessment. It may help to refer to Guidance Note - New and Expectant Mothers during the completion of this form.</li>' +
'<li>You may also find it helpful to refer to other topic related Guidance Notes as you complete this checklist.</li>' +
'<li>The assessment may need to be reviewed more than once as the pregnancy or return to work develops.  It should always be reviewed at the request of the New and Expectant Mother.</li>' +
'<li>The assessment should clearly state what control measures are already in place and indicate the new control measures required - confirmation regarding the implementation of new control measures should be given in the comments section.</li>' +
'</ul>'
WHERE [Id] = 4
GO
