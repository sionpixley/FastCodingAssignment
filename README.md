# FastCodingAssignment

## This repo is only for the coding assignment given to me from FAST Search. NOT for commercial or personal use.

### Assignment Prompt:

A retailer offers a rewards program to its customers, awarding points based on each recorded purchase.
A customer receives 2 points for every dollar spent over $100 in each transaction, plus 1 point for every dollar spent over $50 in each transaction (e.g. a $120 purchase = 2x$20 + 1x$50 = 90 points).
Given a record of every transaction during a three month period, calculate the reward points earned for each customer per month and total.

### About this Repo:

#### Data -

All data is stored in a local SQL Server instance. The code for the creation of the database and its data is located in sionpixley/FastCodingAssignment/SQL-Fast. 
The numbers at the beginning of each file indicates the order for the files to be ran. These files assume that you already have SQL Server installed on your local machine.

#### API -

This repo contains an ASP.NET 5 API. Its files are located in sionpixley/FastCodingAssignment/API-Fast. 
The API has two endpoints: GetPointsForAmount and GetUserPointsForPastNMonths.

##### GetPointsForAmount --

URL: https://localhost:44351/Points/GetForAmount/{amount}
<br>
Parameters: amount (decimal type)
<br>
Response: Total rewarded points for a transaction of that amount (int type).

##### GetUserPointsForPastNMonths --

URL: https://localhost:44351/Points/User/GetForPastNMonths/{numOfMonths}
<br>
Parameters: numOfMonths (int type)
<br>
Response: A list of users and their total points over the past N months, along with a monthly breakdown of how many points they earned per month over that time (IEnumerable\<UserPoints\> type).

#### Demo -

This repo also contains a .mp4 file that shows me running and testing the API. The file can be located at sionpixley/FastCodingAssignment/Demo.
