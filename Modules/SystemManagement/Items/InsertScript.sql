INSERT INTO SYM_ROLE(NAME,CODE) VALUES('User','user')
INSERT INTO SYM_ROLE(NAME,CODE) VALUES('Admin','admin')

INSERT INTO SYM_USER(USERNAME,PASSWORD,FIRSTNAME,LASTNAME,EMAILADDRESS,STATUS)
	VALUES('admin','admin','admin','admin','info@ahioto.com.tr',1)
INSERT INTO SYM_TOKEN(TOKEN, USERNAME,USERID,EXPIREDATE,CREATEDATE)
	VALUES('00000000-0000-0000-0000-000000000000','admin',1,'20501231','20190918')
	
INSERT INTO SYM_USER_ROLE(ROLEID,USERID) VALUES((SELECT TOP 1 ROLEID 
	from SYM_ROLE where 'user'=CODE),(SELECT TOP 1 USERID from SYM_USER where 'admin'=USERNAME))
INSERT INTO SYM_USER_ROLE(ROLEID,USERID) VALUES((SELECT TOP 1 ROLEID 
	from SYM_ROLE where 'admin'=CODE),(SELECT TOP 1 USERID from SYM_USER where 'admin'=USERNAME))

INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Abonelik �demesi','ABN',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('K�sa S�reli Park','KSP',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('�� Y�kama','YKMI',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('D�� Y�kama','YKMD',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('�� D�� Y�kama','YKM',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Avans','AVN',2)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Yemek','YMK',2)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Malzeme','MLZ',2)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Detayl� Temizlik','DTE',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Seramik Kaplama','SKA',1)
INSERT INTO OTO_OPERATIONTYPE(NAME,CODE,ACCOUNTTYPE) VALUES('Masraf','MSRF',2)
