ALTER PROC procGetTotalStudentByStandard
(
@InstitutionAccountId INT
)
AS
BEGIN
	SELECT B.StandardName,COUNT(StudentStandardId)TotalStudent INTO #TEMP FROM [dbo].[Mst_Account] A
	INNER JOIN [Standard] B ON A.StudentStandardId=B.Id
	WHERE A.InstitutionAccountId=@InstitutionAccountId
	GROUP BY B.StandardName,A.StudentStandardId
	ORDER BY A.StudentStandardId
	
	SELECT ISNULL([5Th],0) AS Fifth,ISNULL([6Th],0) AS Sixth,ISNULL([7Th],0) AS Seventh,ISNULL([8Th],0) AS Eight,ISNULL([9Th],0) AS Nineth,ISNULL([10Th],0) AS Tenth
	INTO #STUDENTDATA
	FROM #TEMP
	PIVOT(
	SUM(TotalStudent) FOR StandardName IN ([5Th],[6Th],[7Th],[8Th],[9Th],[10Th])
	)AS PIVOTTABLE
	
	IF((SELECT COUNT(*) FROM #TEMP)<1)
	BEGIN
		INSERT INTO #STUDENTDATA
		SELECT 0,0,0,0,0,0
	END

	SELECT * FROM #STUDENTDATA

	DROP TABLE #STUDENTDATA
	DROP TABLE #TEMP

END
