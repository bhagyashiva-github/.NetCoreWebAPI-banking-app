-- Database: postgres

-- DROP DATABASE IF EXISTS postgres;

CREATE DATABASE postgres
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'C'
    LC_CTYPE = 'C'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE postgres
    IS 'default administrative connection database';

-- Create Tables
CREATE TABLE Clients (
    ClientId SERIAL PRIMARY KEY,
    Name TEXT NOT NULL,
    Email TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Trades (
    TradeId SERIAL PRIMARY KEY,
    Tradetype TEXT,
    Tradestatus TEXT,
    Brokername TEXT,
    Priceperunit NUMERIC,
    Quantity INT,
    Tradedate DATE,
    ClientId INT REFERENCES Clients(ClientId)
);

CREATE TABLE Instruments (
    InstrumentId SERIAL PRIMARY KEY,
    Symbol TEXT NOT NULL,
    Name TEXT,
    Type TEXT,
    Price NUMERIC,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Portfolios (
    PortfolioId SERIAL PRIMARY KEY,
    Name TEXT,
    ClientId INT REFERENCES Clients(ClientId),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);