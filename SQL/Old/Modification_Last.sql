-- Remove duplicate emails (keep lowest user_id)
DELETE FROM users
WHERE user_id NOT IN (
    SELECT MIN(user_id)
    FROM users
    GROUP BY email
);

-- Fix NULL or empty names
UPDATE users SET full_name = 'Unknown'
WHERE full_name IS NULL OR LTRIM(RTRIM(full_name)) = '';

-- Fix NULL complaint fields
UPDATE complaints SET title = 'No Title'
WHERE title IS NULL OR LTRIM(RTRIM(title)) = '';

UPDATE complaints SET description = 'No Description'
WHERE description IS NULL OR LTRIM(RTRIM(description)) = '';

UPDATE complaints SET report_date = GETDATE()
WHERE report_date IS NULL;

-- Fix invalid foreign keys (remove broken rows)
DELETE FROM complaints
WHERE user_id NOT IN (SELECT user_id FROM users)
   OR category_id NOT IN (SELECT category_id FROM categories)
   OR status_id NOT IN (SELECT status_id FROM status);




----------------------------sdsdasd

-- Drop constraint if exists
IF EXISTS (SELECT * FROM sys.key_constraints WHERE name = 'UQ_users_email')
ALTER TABLE users DROP CONSTRAINT UQ_users_email;

-- Alter column
ALTER TABLE users ALTER COLUMN email NVARCHAR(100) NOT NULL;

-- Recreate constraint
ALTER TABLE users ADD CONSTRAINT UQ_users_email UNIQUE (email);


-------------------------------sdsds
SELECT name
FROM sys.key_constraints
WHERE parent_object_id = OBJECT_ID('users');


GO

ALTER TABLE users DROP CONSTRAINT UQ_users_email;
ALTER TABLE users DROP CONSTRAINT UQ__users__AB6E6164810DA264;
-------------------------------asf
DECLARE @sql NVARCHAR(MAX) = '';

SELECT @sql += 'ALTER TABLE users DROP CONSTRAINT ' + kc.name + ';'
FROM sys.key_constraints kc
WHERE kc.parent_object_id = OBJECT_ID('users')
AND kc.type = 'UQ';

PRINT @sql;
EXEC sp_executesql @sql;



---------------------------------

ALTER TABLE users
ALTER COLUMN email NVARCHAR(100) NOT NULL;



-- UNIQUE EMAIL
ALTER TABLE users
ADD CONSTRAINT UQ_users_email UNIQUE (email);

-- NOT NULL USERS
ALTER TABLE users ALTER COLUMN full_name NVARCHAR(100) NOT NULL;
ALTER TABLE users ALTER COLUMN email NVARCHAR(100) NOT NULL;
ALTER TABLE users ALTER COLUMN password NVARCHAR(100) NOT NULL;

-- NOT NULL COMPLAINTS
ALTER TABLE complaints ALTER COLUMN title NVARCHAR(200) NOT NULL;
ALTER TABLE complaints ALTER COLUMN description NVARCHAR(MAX) NOT NULL;
ALTER TABLE complaints ALTER COLUMN report_date DATE NOT NULL;

-- DEFAULT STATUS
ALTER TABLE complaints
ADD CONSTRAINT DF_status DEFAULT 1 FOR status_id;

-- FOREIGN KEYS
ALTER TABLE complaints
ADD CONSTRAINT FK_user FOREIGN KEY (user_id) REFERENCES users(user_id);

ALTER TABLE complaints
ADD CONSTRAINT FK_category FOREIGN KEY (category_id) REFERENCES categories(category_id);

ALTER TABLE complaints
ADD CONSTRAINT FK_status FOREIGN KEY (status_id) REFERENCES status(status_id);

-- CHECK CONSTRAINT (TITLE LENGTH)
ALTER TABLE complaints
ADD CONSTRAINT CHK_title CHECK (LEN(title) >= 5);




----------Final Constrains---------------


-- =========================
-- USERS TABLE FIX
-- =========================

-- Drop all UNIQUE constraints first (safe reset)
DECLARE @sql NVARCHAR(MAX) = '';
SELECT @sql += 'ALTER TABLE users DROP CONSTRAINT ' + kc.name + ';'
FROM sys.key_constraints kc
WHERE kc.parent_object_id = OBJECT_ID('users') AND kc.type = 'UQ';

EXEC sp_executesql @sql;

-- Make columns NOT NULL
ALTER TABLE users ALTER COLUMN full_name NVARCHAR(100) NOT NULL;
ALTER TABLE users ALTER COLUMN email NVARCHAR(100) NOT NULL;
ALTER TABLE users ALTER COLUMN password NVARCHAR(100) NOT NULL;

-- Add UNIQUE constraint again
ALTER TABLE users ADD CONSTRAINT UQ_users_email UNIQUE (email);


-- =========================
-- COMPLAINTS TABLE FIX
-- =========================

-- Make columns NOT NULL
ALTER TABLE complaints ALTER COLUMN title NVARCHAR(200) NOT NULL;
ALTER TABLE complaints ALTER COLUMN description NVARCHAR(MAX) NOT NULL;
ALTER TABLE complaints ALTER COLUMN report_date DATE NOT NULL;

-- Default constraint (check first)
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_status')
ALTER TABLE complaints ADD CONSTRAINT DF_status DEFAULT 1 FOR status_id;

-- Foreign Keys (check first)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_user')
ALTER TABLE complaints
ADD CONSTRAINT FK_user FOREIGN KEY (user_id) REFERENCES users(user_id);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_category')
ALTER TABLE complaints
ADD CONSTRAINT FK_category FOREIGN KEY (category_id) REFERENCES categories(category_id);

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_status')
ALTER TABLE complaints
ADD CONSTRAINT FK_status FOREIGN KEY (status_id) REFERENCES status(status_id);

-- Check constraint
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CHK_title')
ALTER TABLE complaints
ADD CONSTRAINT CHK_title CHECK (LEN(title) >= 5);


SELECT * FROM complaints
WHERE LEN(title) < 5;


UPDATE complaints
SET title = 'Updated Title'
WHERE LEN(title) < 5;


ALTER TABLE complaints
ADD CONSTRAINT CHK_title CHECK (LEN(title) >= 5);


---------add admin-------------

INSERT INTO users (full_name, email, password, role)
VALUES ('Admin', 'admin2@test.com', '123456', 'Admin');


SELECT user_id, full_name, email, password, role 
FROM users;

SELECT * FROM status