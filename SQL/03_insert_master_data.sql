USE ComplaintDB;
GO

-- Categories
INSERT INTO categories (category_name)
VALUES
('Waste'),
('Pollution'),
('Road Damage'),
('Facility Issue');

-- Status
INSERT INTO status (status_name)
VALUES
('Submitted'),
('Verified'),
('In Process'),
('Resolved'),
('Rejected');

-- Users
INSERT INTO users (full_name, email, password, role)
VALUES
('Admin User', 'admin@system.com', 'admin123', 'Admin'),
('John Citizen', 'john@gmail.com', '123456', 'Citizen'),
('Sara Citizen', 'sara@gmail.com', '123456', 'Citizen');
GO