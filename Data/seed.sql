INSERT INTO Clients (Name, Email) VALUES
('Alice Kapoor', 'alice@example.com'),
('Ravi Menon', 'ravi@example.com');

INSERT INTO Trades (Tradetype, Tradestatus, Brokername, Priceperunit, Quantity, Tradedate, ClientId)
VALUES 
('Buy', 'Executed', 'AxisSec', 102.5, 50, '2025-06-30', 1),
('Sell', 'Pending', 'Zerodha', 98.3, 30, '2025-06-28', 2);

INSERT INTO Instruments (Symbol, Name, Type, Price)
VALUES
('ICICIBANK', 'ICICI Bank Ltd.', 'Equity', 920.50),
('TCS', 'Tata Consultancy Services', 'Equity', 3670.30);

INSERT INTO Portfolios (Name, ClientId)
VALUES
('Growth Portfolio', 1),
('Retirement Portfolio', 2);