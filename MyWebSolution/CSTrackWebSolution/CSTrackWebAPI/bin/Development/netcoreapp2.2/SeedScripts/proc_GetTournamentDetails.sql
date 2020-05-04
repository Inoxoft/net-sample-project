IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetTournamentDetails]')
AND type in (N'P'))
DROP Procedure [dbo].[proc_GetTournamentDetails]

GO

CREATE PROCEDURE dbo.proc_GetTournamentDetails

@Id uniqueidentifier

AS
BEGIN
SET NOCOUNT ON;

;WITH x AS
(
    SELECT Id, Name, ParentTournamentId, [Level]
    FROM Tournaments AS T WHERE Id=@Id
    UNION ALL
    SELECT t.Id, t.Name, t.ParentTournamentId, T.[Level]
    FROM x INNER JOIN dbo.Tournaments AS t
    ON t.ParentTournamentId = x.ID
)
SELECT x.Id, x.Name, x.ParentTournamentId, x.[Level], pt.PlayerId, p.Name as [PlayerName]  FROM x 
left join PlayerTournament AS pt ON pt.TournamentId = x.Id
left join Players AS p ON p.Id = pt.PlayerId
ORDER BY [level]
OPTION (MAXRECURSION 5);

END