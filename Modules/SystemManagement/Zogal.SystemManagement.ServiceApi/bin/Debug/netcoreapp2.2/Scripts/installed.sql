
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_8BD98013]') and parent_object_id = OBJECT_ID(N'SYM_USER_ROLE'))
alter table SYM_USER_ROLE  drop constraint FK_8BD98013


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_19FDA34A]') and parent_object_id = OBJECT_ID(N'SYM_USER_ROLE'))
alter table SYM_USER_ROLE  drop constraint FK_19FDA34A


    if exists (select * from dbo.sysobjects where id = object_id(N'SYM_MESSAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYM_MESSAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYM_ROLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYM_ROLE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYM_TOKEN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYM_TOKEN

    if exists (select * from dbo.sysobjects where id = object_id(N'SYM_USER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYM_USER

    if exists (select * from dbo.sysobjects where id = object_id(N'SYM_USER_ROLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYM_USER_ROLE

    create table SYM_MESSAGE (
        MESSAGEID BIGINT IDENTITY NOT NULL,
       NAME NVARCHAR(255) null,
       EMAIL NVARCHAR(255) null,
       PHONENUMBER NVARCHAR(255) null,
       SUBJECT NVARCHAR(255) null,
       BODY NVARCHAR(255) null,
       SOURCE NVARCHAR(255) null,
       primary key (MESSAGEID)
    )

    create table SYM_ROLE (
        ROLEID BIGINT IDENTITY NOT NULL,
       NAME NVARCHAR(255) null,
       CODE NVARCHAR(255) null,
       primary key (ROLEID)
    )

    create table SYM_TOKEN (
        TID BIGINT IDENTITY NOT NULL,
       TOKEN UNIQUEIDENTIFIER null,
       USERNAME NVARCHAR(255) null,
       USERID BIGINT null,
       EXPIREDATE DATETIME2 null,
       CREATEDATE DATETIME2 null,
       primary key (TID)
    )

    create table SYM_USER (
        USERID BIGINT IDENTITY NOT NULL,
       USERNAME NVARCHAR(255) null,
       PASSWORD NVARCHAR(255) null,
       FIRSTNAME NVARCHAR(255) null,
       LASTNAME NVARCHAR(255) null,
       EMAILADDRESS NVARCHAR(255) null,
       STATUS INT null,
       primary key (USERID)
    )

    create table SYM_USER_ROLE (
        USERID BIGINT not null,
       ROLEID BIGINT not null
    )

    alter table SYM_USER_ROLE 
        add constraint FK_8BD98013 
        foreign key (ROLEID) 
        references SYM_ROLE

    alter table SYM_USER_ROLE 
        add constraint FK_19FDA34A 
        foreign key (USERID) 
        references SYM_USER
