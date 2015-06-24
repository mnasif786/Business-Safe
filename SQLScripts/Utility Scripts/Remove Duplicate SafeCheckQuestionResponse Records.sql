use BusinessSafe
go

--DELETE FROM safecheckquestionresponse
--WHERE CAST(id AS BINARY(16)) NOT IN
--(
--	SELECT MIN(CAST(id AS BINARY(16)))
--	FROM SafeCheckQuestionResponse
--	WHERE Id NOT IN (SELECT ResponseId FROM SafeCheckChecklistAnswer)
--	GROUP BY  questionid, title
--)

-- check for duplicate responses
select questionid, title, count(*) as count
	from safecheckquestionresponse 
	group by questionid, title
	HAVING COUNT(*) > 1

-- show duplicates and originals
select * from safecheckquestionresponse r
	inner join (
	select questionid, title, count(*) as count
	from safecheckquestionresponse 
	group by questionid, title
	HAVING COUNT(*) > 1) t
	on t.questionid = r.questionid
	where cast(CONVERT(varchar,createdon, 112) as datetime) > '2013-12-04 00:00:00.000'
	order by r.QuestionId

-- show answers using duplicates
select * from safecheckchecklistanswer 
where responseid  in(
select id from safecheckquestionresponse r
	inner join (
	select questionid, title, count(*) as count
	from safecheckquestionresponse 
	group by questionid, title
	HAVING COUNT(*) > 1) t
	on t.questionid = r.questionid
	where cast(CONVERT(varchar,createdon, 112) as datetime) > '2013-12-04 00:00:00.000'
)

-- update answer to use original response
update a
	set a.responseid = (select id from safecheckquestionresponse r2 where r2.QuestionId = a.QuestionId and Title = 'Not Applicable' and a.ResponseId != r2.Id)
	from safecheckchecklistanswer a
where responseid  in(
select id from safecheckquestionresponse r
	inner join (
	select questionid, title, count(*) as count
	from safecheckquestionresponse 
	group by questionid, title
	HAVING COUNT(*) > 1) t
	on t.questionid = r.questionid
where cast(CONVERT(varchar,createdon, 112) as datetime) > '2013-12-09 00:00:00.000'
)

-- delete duplicate
delete from safecheckquestionresponse where id in (
select id from safecheckquestionresponse r
	inner join (
	select questionid, title, count(*) as count
	from safecheckquestionresponse 
	group by questionid, title
	HAVING COUNT(*) > 1) t
	on t.questionid = r.questionid
where cast(CONVERT(varchar,createdon, 112) as datetime) > '2013-12-09 00:00:00.000'
)


--update SafeCheckCheckList set LastModifiedOn = GETDATE() where id = 'DEE0BA73-DDA7-445A-80B0-734C95D64353'
--select * from safecheckchecklist where id = 'DEE0BA73-DDA7-445A-80B0-734C95D64353'