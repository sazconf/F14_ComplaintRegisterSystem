SELECT 
    c.complaint_id,
    u.full_name AS Citizen,
    cat.category_name AS Category,
    s.status_name AS Status,
    c.title,
    c.description,
    c.report_date,
    c.rejection_reason
FROM dbo.complaints c
INNER JOIN dbo.users u ON c.user_id = u.user_id
INNER JOIN dbo.categories cat ON c.category_id = cat.category_id
INNER JOIN dbo.status s ON c.status_id = s.status_id;