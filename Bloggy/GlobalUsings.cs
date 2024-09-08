global using System;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Linq;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Bloggy.DataAccessLayer.Models;
global using Bloggy.PresentationLayer.Controllers;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Bloggy.DataAccessLayer.Repositories.IRepositories;
global using Bloggy.BussinessLogicLayer.DTOs;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Authorization;
global using Bloggy.BussinessLogicLayer.Services;
global using Bloggy.BussinessLogicLayer.Services.IServices;
global using Bloggy.DataAccessLayer.Repositories;
global using Bloggy.BussinessLogicLayer.DTOs.PostDto;
global using Bloggy.BussinessLogicLayer.DTOs.CategoryDto;
global using Bloggy.BussinessLogicLayer.DTOs.CommentDto;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using Bloggy.BussinessLogicLayer.DTOs.AuthDto;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Bloggy.BussinessLogicLayer.DTOs.UserDto;



