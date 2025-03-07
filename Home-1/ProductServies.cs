﻿using System;
using System.Data.SqlClient;

namespace Home_1;

public class ProductServies
{
    private string _conStr;

    public ProductServies(string conStr)
    {
        _conStr = conStr;
    }

    public void CreateProductTable(string name)
    {
        using (var connect = new SqlConnection(_conStr))
        {
            try
            {
                connect.Open();
                var query = "Create table [Products](Id int primary key identity,[Name] nvarchar(max) not null check([Name] != ''),[Salary] money not null check([Salary > 0]),[Description] nvarchar(max) not null check([Description] != '') );";
                var command = new SqlCommand(query, connect);
                command.ExecuteNonQuery();
                Console.WriteLine("Table created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    public void AddProduct(Product product)
    {
        using (var connect = new SqlConnection(_conStr))
        {
            try
            {
                connect.Open();
                var query = $"insert [Products] values({product.Id},{product.Name},{product.Price},{product.Description})";
                var command = new SqlCommand(query , connect);
                command.ExecuteNonQuery();
                Console.WriteLine("Adding successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void DeleteProduct(int id)
    {
        using(var connect = new SqlConnection(_conStr))
        {
            try
            {
                connect.Open();
                var query = $"delete from [Products] where id={id})";
                var command = new SqlCommand(query, connect);
                command.ExecuteNonQuery();
                Console.WriteLine("Delete successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public void UpdateProduct(int id, Product newProduct)
    {
        using (var connect = new SqlConnection(_conStr))
        {
            try
            {
                connect.Open();
                var query = $"UPDATE Products SET Name = {newProduct.Name}, Price = {newProduct.Price}, Description = {newProduct.Description} WHERE Id = {newProduct.Id}";
                var command = new SqlCommand(query, connect);
                command.ExecuteNonQuery();
                Console.WriteLine("Update successful");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

    public IEnumerable<Product> GetTable()
    {
        using (var connect = new SqlConnection(_conStr))
        {
            var tempProducts = new List<Product>();
            try
            {
                var getTableQuery = "select * from [Product]";
                var getTable = new SqlCommand(getTableQuery, connect);
                using (var reader = getTable.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        var tempProduct = new Product();
                        tempProduct.Id = reader.GetFieldValue<int>(0);
                        tempProduct.Name = reader.GetFieldValue<string>(1);
                        tempProduct.Price = reader.GetFieldValue<decimal>(2);
                        tempProduct.Description = reader.GetFieldValue<string>(4);
                        tempProducts.Add(tempProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return tempProducts;
        }
    }
}
