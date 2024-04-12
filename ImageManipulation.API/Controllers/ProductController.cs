using ImageManipulation.Data.Repositories;
using ImageManipulation.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageManipulation.API.Controllers;

[Route("api/products")]
[ApiController]
public class ImageManipulationController(IFileService fileService, IProductRepository productRepo) : ControllerBase
{

}