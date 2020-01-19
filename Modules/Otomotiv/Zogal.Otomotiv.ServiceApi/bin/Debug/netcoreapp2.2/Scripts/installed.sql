
    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_COUNTER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_COUNTER

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_CUSTOMER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_CUSTOMER

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_OPERATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_OPERATION

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_OPERATIONTYPE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_OPERATIONTYPE

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_PRICEMAP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_PRICEMAP

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_STOCK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_STOCK

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_SUBSCRIBER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_SUBSCRIBER

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_TIP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_TIP

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_VEHICLETYPE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_VEHICLETYPE

    if exists (select * from dbo.sysobjects where id = object_id(N'OTO_WASHTYPE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table OTO_WASHTYPE

    create table OTO_COUNTER (
        ID BIGINT IDENTITY NOT NULL,
       TYPE NVARCHAR(255) null,
       STARTVALUE DECIMAL(19,5) null,
       STOPVALUE DECIMAL(19,5) null,
       SPENTVALUE DECIMAL(19,5) null,
       [DATE] BIGINT null,
       STATUS INT null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       primary key (ID)
    )

    create table OTO_CUSTOMER (
        CUSTOMERID BIGINT IDENTITY NOT NULL,
       FIRSTNAME NVARCHAR(255) null,
       LASTNAME NVARCHAR(255) null,
       GENDER NVARCHAR(255) null,
       COMPANYNAME NVARCHAR(255) null,
       PHONENUMBER NVARCHAR(255) null,
       STATUS INT null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       primary key (CUSTOMERID)
    )

    create table OTO_OPERATION (
        OPERATIONID BIGINT IDENTITY NOT NULL,
       PLATE NVARCHAR(255) null,
       OPERATIONDATE BIGINT null,
       PERIODDATE BIGINT null,
       OPERATIONTYPEID BIGINT null,
       PAYMENTMETHOD INT null,
       OPEARATIONAMOUNT DECIMAL(19,5) null,
       CALCULATEDAMOUNT DECIMAL(19,5) null,
       TIPAMOUNT DECIMAL(19,5) null,
       FIRSTNAME NVARCHAR(255) null,
       LASTNAME NVARCHAR(255) null,
       GENDER NVARCHAR(255) null,
       PHONENUMBER NVARCHAR(255) null,
       VEHICLETYPE NVARCHAR(255) null,
       VEHICLEMODEL NVARCHAR(255) null,
       VEHICLEBRAND NVARCHAR(255) null,
       STATUS INT null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       DESCRIPTION NVARCHAR(255) null,
       primary key (OPERATIONID)
    )

    create table OTO_OPERATIONTYPE (
        OPERATIONTYPEID BIGINT IDENTITY NOT NULL,
       NAME NVARCHAR(255) null,
       CODE NVARCHAR(255) null,
       ACCOUNTTYPE INT null,
       primary key (OPERATIONTYPEID)
    )

    create table OTO_PRICEMAP (
        ID BIGINT IDENTITY NOT NULL,
       VEHICLETYPE NVARCHAR(255) null,
       OPERATIONTYPEID BIGINT null,
       DEFAULTPRICE DECIMAL(19,5) null,
       STATUS INT null,
       primary key (ID)
    )

    create table OTO_STOCK (
        ID BIGINT IDENTITY NOT NULL,
       BARCODENUMBER BIGINT null,
       NAME NVARCHAR(255) null,
       UNITPRICE DECIMAL(19,5) null,
       NUMBEROFUNIT DECIMAL(19,5) null,
       STATUS INT null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       primary key (ID)
    )

    create table OTO_SUBSCRIBER (
        SUBSCRIBERID BIGINT IDENTITY NOT NULL,
       CUSTOMERID BIGINT null,
       STARTDATE BIGINT null,
       LASTSTARTDATE BIGINT null,
       STATUS INT null,
       MONTHLYSUBSCRIPTION DECIMAL(19,5) null,
       DEPOSIT DECIMAL(19,5) null,
       PLATE NVARCHAR(255) null,
       BRAND NVARCHAR(255) null,
       MODEL NVARCHAR(255) null,
       TYPE NVARCHAR(255) null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       primary key (SUBSCRIBERID)
    )

    create table OTO_TIP (
        ID BIGINT IDENTITY NOT NULL,
       TIPAMOUNT DECIMAL(19,5) null,
       WORKERNUMBER DECIMAL(19,5) null,
       OPERATIONDATE BIGINT null,
       PERIODDATE BIGINT null,
       STATUS INT null,
       CREATEDBY NVARCHAR(255) null,
       CREATEDBYFULLNAME NVARCHAR(255) null,
       CREATEDDATE BIGINT null,
       UPDATEDBY NVARCHAR(255) null,
       UPDATEDBYFULLNAME NVARCHAR(255) null,
       UPDATEDDATE BIGINT null,
       DESCRIPTION NVARCHAR(255) null,
       primary key (ID)
    )

    create table OTO_VEHICLETYPE (
        ID BIGINT IDENTITY NOT NULL,
       NAME NVARCHAR(255) null,
       DEFAULTPRİCE DECIMAL(19,5) null,
       primary key (ID)
    )

    create table OTO_WASHTYPE (
        ID BIGINT IDENTITY NOT NULL,
       NAME NVARCHAR(255) null,
       DEFAULTPRİCE DECIMAL(19,5) null,
       PROCESSDURATION DECIMAL(19,5) null,
       primary key (ID)
    )
