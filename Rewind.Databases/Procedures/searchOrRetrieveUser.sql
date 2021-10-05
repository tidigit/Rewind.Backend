   
   
   
   
   
   
   
   
alter procedure searchAndRetrieveUser
   @username varchar(500) = '',
   @email varchar(500) = '',
   @phone varchar(500) = ''
   as
   /*
 Created by: Aravind Samala
 Created at: Oct 03,2021
 Reason: account flows
 ---Versioning---
 Modified by        Modified at     Reason
 ---example usage---
exec searchAndRetrieveUser '','thyaravind','4808683909';
 */
   BEGIN  
		select firstName, lastName, email, username, phone, passwordHash, userTimezone from users with (nolock)
		where (username is not null and username = @username) or (email is not null and email = @email) or (phone is not null and phone = @phone);
   END  

   
   
   

   exec searchAndRetrieveUser '','','4808683909';

   
   
   
   create procedure searchAndRetrieveUser
   @username varchar = '',
   @email varchar = '',
   @phone varchar = ''
   as
   /*
 Created by: Aravind Samala
 Created at: Oct 03,2021
 Reason: account flows
 ---Versioning---
 Modified by        Modified at     Reason
 ---example usage---
 call createUser('Aravind','Samala','ar@blanklabs.org','thyaravind','hakuna','4808683909');
 */
   BEGIN  
	   IF EXISTS (SELECT id FROM users with (nolock) WHERE email = @email or phone = @phone or username = @username)
		BEGIN
		select firstName, lastName, email, username, phone, passwordHash from users with (nolock) where email = @email or phone = @phone or username = @username;
		END
		ELSE
		BEGIN
		--do what needs to be done if not
		END

   END  
