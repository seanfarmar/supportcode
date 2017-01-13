/****** Script for SelectTopNRows command from SSMS  ******/
SELECT Count(Id) as Dublications
      ,Id
	  ,(Max(RowVersion) - Min(RowVersion)) as DeltaRowVersion
  FROM [WorkFlows_DEV].[dbo].[CSReportingPOC]
  group by Id
  HAVING Count(Id) > 1

SELECT *
  FROM [TimeoutEntity]
  where Destination = 'flowmanagerpoc'

SELECT * from FlowInstanceSagaDataPOC

  /*
  delete
    FROM [WorkFlows_DEV].[dbo].[TimeoutEntity]
  where Destination = 'flowmanagerpoc'

  drop table [CSReportingPOC]
  drop table [FlowInstanceSagaDataPOC]
  drop table [FlowmanagerPOC]  
  drop table [FlowmanagerPOC.Retries]
  drop table [FlowmanagerPOC.Timeouts]
  drop table [FlowmanagerPOC.TimeoutsDispatcher]
  drop table [FlowmanagerPOC.WS-DMITRYSI-7]
  */
