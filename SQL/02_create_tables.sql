USE ComplaintDB;
GO

CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL
);
GO

CREATE TABLE categories (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE
);
GO

CREATE TABLE status (
    status_id INT IDENTITY(1,1) PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE complaints (
    complaint_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    category_id INT NOT NULL,
    status_id INT NOT NULL,
    title VARCHAR(150) NOT NULL,
    description VARCHAR(500) NOT NULL,
    report_date DATE NOT NULL,
    rejection_reason VARCHAR(255) NULL,

    CONSTRAINT FK_Complaint_User
        FOREIGN KEY (user_id) REFERENCES users(user_id),

    CONSTRAINT FK_Complaint_Category
        FOREIGN KEY (category_id) REFERENCES categories(category_id),

    CONSTRAINT FK_Complaint_Status
        FOREIGN KEY (status_id) REFERENCES status(status_id)
);
GO