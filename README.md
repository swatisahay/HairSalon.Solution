# Hair Salon
##### Database for a hair salon

#### By Swati Sahay, 07.13.2018

## Description

A database that stores list of stylists and their clients for a hair salon.

## Setup

* CREATE DATABASE swati_sahay;
* USE swati_sahay_salon;
* CREATE TABLE stylists (id serial PRIMARY KEY, stylist_name , Stylist_details, VARCHAR(255));
* CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id VARCHAR(255));

## Spec

* Let user see a list of all stylists
* Let user select a stylist, see their details, and see a list of all clients that belong to that stylist
* Let user add new stylists
* Let the user add new clients to a specific stylist. User should not be able to add a client if no stylist have been added.

## Technologies Used

Application: CSharp, netcoreapp1.1, Razor, MAMP, MySQL

## Support and Contact

For any questions or support details, please email:
swatiranjan0111@gmail.com



### Legal

Copyright (c) 2018 **Swati Sahay**

This software is licensed under the MIT license.
