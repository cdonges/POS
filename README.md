# Point of Sale discount test
## Overview 
You are a developer working on a Retail Point of Sale (POS) application. The key functionality of the application is the ability for a user to create a new sale, add products to the sale (as sale items), set individual discounts for each of the sale items, take payment from the customer and process the sale. 

You are a member of the team implementing a new Bulk Discounting feature in the application. This new feature will allow a user to bulk apply discounts to all items in a sale by specifying the new total price for the sale. The application will then calculate the discounts to be applied to each item in the sale so that the total price matches the value entered. 

## Requirements 
A user can enter any positive monetary value as the new Sale Total 

The discount value should be apportioned to each of the items in the sale using the item’s undiscounted price as the basis for the apportionment. The calculation of the discount to applied to each item is as follows: 
Item Discount = Item Quantity * Item Unit Price * Total Discount/Undiscounted Sale Total 

A product can be configured to have a Minimum Price. Any sale item for a product with a Minimum Price cannot be discounted below its Minimum Price 

In the case where a product’s Minimum Price prevents the full discounting of a sale item, the remaining discount should be apportioned across the remaining items in the sale 

In the event where the full discount is not possible because all items in the sale have a Minimum Price higher than the desired target price, all items should be discounted to their respective Minimum Price values 

Should there be a rounding difference after all of the discounts have been applied, the item with the largest undiscounted price should be adjusted by the amount of the rounding error to ensure that the Sale Total matches the value entered by the user 
