-- Insert users
INSERT INTO Users (Name, Email, PasswordHash, PasswordSalt)
VALUES 
('Malik', 'malik@example.com', 0x123456, 0x123456), -- Replace with actual hash and salt
('Ali', 'ali@example.com', 0x654321, 0x654321); -- Replace with actual hash and salt

-- Get UserIds for Malik and Ali
DECLARE @UserIdMalik INT = (SELECT UserId FROM Users WHERE Name = 'Malik');
DECLARE @UserIdAli INT = (SELECT UserId FROM Users WHERE Name = 'Ali');

-- Insert accounts for Malik and Ali
INSERT INTO Accounts (UserId, Balance, AccountNumber)
VALUES 
(@UserIdMalik, 10000, 'ACC1001'),
(@UserIdAli, 5000, 'ACC1002');

-- Get AccountIds for Malik and Ali
DECLARE @AccountIdMalik INT = (SELECT AccountId FROM Accounts WHERE UserId = @UserIdMalik);
DECLARE @AccountIdAli INT = (SELECT AccountId FROM Accounts WHERE UserId = @UserIdAli);

-- Insert transactions for Malik
INSERT INTO Transactions (AccountId, Amount, Date, Description)
VALUES 
(@AccountIdMalik, 500, GETDATE(), 'Deposit'),
(@AccountIdMalik, -200, GETDATE(), 'Withdrawal');

-- Insert transactions for Ali
INSERT INTO Transactions (AccountId, Amount, Date, Description)
VALUES 
(@AccountIdAli, 1000, GETDATE(), 'Deposit'),
(@AccountIdAli, -150, GETDATE(), 'Withdrawal');

-- Insert transfer from Malik to Ali
INSERT INTO Transfers (FromAccountId, ToAccountId, Amount, Date, Description)
VALUES 
(@AccountIdMalik, @AccountIdAli, 250, GETDATE(), 'Transfer to Ali');
