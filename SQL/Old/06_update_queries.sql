UPDATE dbo.complaints
SET user_id = @user_id,
    category_id = @category_id,
    status_id = @status_id,
    title = @title,
    description = @description,
    report_date = @report_date,
    rejection_reason = @rejection_reason
WHERE complaint_id = @complaint_id;