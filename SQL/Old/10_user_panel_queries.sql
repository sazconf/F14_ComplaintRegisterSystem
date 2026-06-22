-- Load Citizens
SELECT user_id, full_name
FROM dbo.users
WHERE role = 'Citizen';


-- Load Categories
SELECT category_id, category_name
FROM dbo.categories;


-- Submit Complaint
INSERT INTO dbo.complaints
(
    user_id,
    category_id,
    status_id,
    title,
    description,
    report_date,
    rejection_reason
)
VALUES
(
    @user_id,
    @category_id,
    1,
    @title,
    @description,
    @report_date,
    ''
);


-- View My Complaints
SELECT
    c.complaint_id,
    c.title,
    c.description,
    cat.category_name AS Category,
    s.status_name AS Status,
    c.report_date,
    c.rejection_reason
FROM dbo.complaints c
INNER JOIN dbo.categories cat
    ON c.category_id = cat.category_id
INNER JOIN dbo.status s
    ON c.status_id = s.status_id
WHERE c.user_id = @user_id;