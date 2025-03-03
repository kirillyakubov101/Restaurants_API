BEGIN TRAN

DECLARE @UserId NVARCHAR(400) = '04c32d52-8c9f-4e3d-8baa-409fa2b4962c'

INSERT INTO [dbo].[Restaurants] 
    ([Name], [Description], [Category], [HasDelivery], [ContactEmail], [ContactNumber], [Address_PostalCode], [OwnerId])
VALUES
    ('NewRest 1', 'NewRest', 'Indian', 1, 'email1@example.com', '1234501', '33-445', @UserId),
    ('NewRest 2', 'NewRest', 'Indian', 1, 'email2@example.com', '1234502', '33-445', @UserId),
    ('NewRest 3', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 4', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 5', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 6', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 7', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 8', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 9', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 10', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 11', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 12', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 13', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 14', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),
    ('NewRest 15', 'NewRest', 'Indian', 1, 'email3@example.com', '1234503', '33-445', @UserId),

    -- Repeat similar values for 500 entries
    ('NewRest 503', 'NewRest', 'Indian', 1, 'email500@example.com', '1234500', '33-445', @UserId);

COMMIT TRAN;
