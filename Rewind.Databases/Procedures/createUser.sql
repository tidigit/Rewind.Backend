

alter procedure createUser
   @firstName varchar(500) = '',
   @lastname varchar(500) = '',
   @username varchar(50) = null,
   @email varchar(500) = null,
   @passwordhash varchar(500) = null,
   @phone varchar(500) = null
   as
   /*
 Created by: Aravind Samala
 Created at: Oct 03,2021
 Reason: signup flows
 ---Versioning---
 Modified by        Modified at     Reason
 ---example usage---
 execute createUser 'Aravind','Samala','ar@blanklabs.org','thyaravind','hakuna','4808683909';
 */
   BEGIN  
   insert into [dbo].[users] (firstName, lastName, userName, email, phone, passwordHash)  
   values (@firstName, @lastName, @userName, @email, @phone, @passwordHash)  
   SELECT SCOPE_IDENTITY() as insertedId;
   END  




   exec createUser 'Aravind','Samala','thyaravind','ar@blanklabs.org','hakuna','4808683909';
