using ImageManipulation.Data.Models;
using ImageManipulation.Data.Models.DTO;
using ImageManipulation.Data.Repositories;
using ImageManipulation.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageManipulation.API.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController(IFileService fileService, IProductRepository productRepo, ILogger<ProductController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] ProductDTO productToAdd)
    {
        try
        {
            if (productToAdd.ImageFile?.Length > 1 * 1024 * 1024)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
            string createdImageName = await fileService.SaveFileAsync(productToAdd.ImageFile, allowedFileExtentions);

            // mapping `ProductDTO` to `Product` manually. You can use automapper.
            var product = new Product
            {
                ProductName = productToAdd.ProductName,
                ProductImage = createdImageName
            };
            var createdProduct = await productRepo.AddProductAsync(product);
            return CreatedAtAction(nameof(CreateProduct), createdProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    // api/products/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductUpdateDTO productToUpdate)
    {
        try
        {
            if (id != productToUpdate.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"id in url and form body does not match.");
            }

            var existingProduct = await productRepo.FindProductByIdAsync(id);
            if (existingProduct == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
            }
            string oldImage = existingProduct.ProductImage;
            if (productToUpdate.ImageFile != null)
            {
                if (productToUpdate.ImageFile?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
                string createdImageName = await fileService.SaveFileAsync(productToUpdate.ImageFile, allowedFileExtentions);
                productToUpdate.ProductImage = createdImageName;
            }

            // mapping `ProductDTO` to `Product` manually. You can use automapper.
            existingProduct.Id = productToUpdate.Id;
            existingProduct.ProductName = productToUpdate.ProductName;
            existingProduct.ProductImage = productToUpdate.ProductImage;

            var updatedProduct = await productRepo.UpdateProductAsync(existingProduct);

            // if image is updated, then we have to delete old image from directory
            if (productToUpdate.ImageFile != null)
                fileService.DeleteFile(oldImage);

            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    // api/products/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var existingProduct = await productRepo.FindProductByIdAsync(id);
            if (existingProduct == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
            }

            await productRepo.DeleteProductAsync(existingProduct);
            // After deleting product from database,remove file from directory.
            fileService.DeleteFile(existingProduct.ProductImage);
            return NoContent();  // return 204
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // api/products/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await productRepo.FindProductByIdAsync(id);
        if (product == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, $"Product with id: {id} does not found");
        }
        return Ok(product);
    }


    // api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await productRepo.GetProductsAsync();
        return Ok(products);
    }


}