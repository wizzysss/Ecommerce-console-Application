create database EcommerceApp

use EcommerceApp

-- Create customers table
CREATE TABLE customers (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    Cust_name VARCHAR(25),
    email VARCHAR(50),
    password VARCHAR(25)
);


-- Create products table
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(25),
    price INT,
    [description] TEXT,
    stockQuantity INT
);

-- Create cart table
CREATE TABLE cart (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT ,
    product_id INT,
    quantity INT,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id) on delete cascade,
    FOREIGN KEY (product_id) REFERENCES products(product_id) on delete cascade
);

-- Create orders table
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT,
    order_date DATE,
    total_price DECIMAL(10, 2),
    shipping_address TEXT,
    FOREIGN KEY (customer_id) REFERENCES customers(customer_id)  on delete cascade
);

-- Create order_items table
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    product_id INT,
    quantity INT,
    FOREIGN KEY (order_id) REFERENCES orders(order_id) on delete cascade,
    FOREIGN KEY (product_id) REFERENCES products(product_id) on delete cascade
);