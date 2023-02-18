using Xunit;
using FakeItEasy;
using TaskTracker.Api.Controllers;
using AutoMapper;
using TaskTracker.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Model;
using TaskTracker.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskTracker.Model.DTO;
using TaskTracker.Data.Repository.IRepository;
using FluentAssertions;
using System;

namespace TaskTracker.Tests.Controller
{
    public class ProjectsControllerTests
    {

        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ProjectsControllerTests()
        {
            unitOfWork = A.Fake<IUnitOfWork>();
            mapper = A.Fake<IMapper>();
        }          

        [Fact]
        public async void ProjectsController_Get_ReturnsOk()
        {
            //Arrange
            var projects = A.Fake<IEnumerator<Project>>();
            var projectsDTOList = A.Fake<List<ProjectDTO>>();
            A.CallTo(() => mapper.Map<List<ProjectDTO>>(projects)).Returns(projectsDTOList);
            var controller = new ProjectsController(unitOfWork, mapper);

            //Act
            var result = await controller.Get();

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }




        [Fact]
        public async void ProjectsController_GetId_ReturnOk()
        {
            //Arrane
            int id = 1;
            var project = A.Fake<Project>();
            var projectDTO = A.Fake<ProjectDTO>();
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(project);
            A.CallTo(() => mapper.Map<ProjectDTO>(project)).Returns(projectDTO);
            var controller = new ProjectsController(unitOfWork,mapper);

            //Act
            var result = await controller.Get(id);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ProjectsController_GetId_ReturnNotFound()
        {
            //Arrane
            int id = 1;
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(null);
            var controller = new ProjectsController(unitOfWork, mapper);

            //Act
            var result = await controller.Get(id);

            //Assert
            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }




        [Fact]
        public async void projectController_GetProjectTasks_ReturnOk()
        {
            int id = 0;
            A.CallTo(() => unitOfWork.Project.Exist(id)).Returns(true);
            var tasks = A.Fake<ICollection<Model.Task>>();
            var tasksDTO = A.Fake<ICollection<TaskDTO>>();
            A.CallTo(() => unitOfWork.Project.GetProjectTasks(id)).Returns(tasks);
            A.CallTo(() => mapper.Map<ICollection<TaskDTO>>(tasks)).Returns(tasksDTO);
            var controller = new ProjectsController(unitOfWork, mapper);


            var result = await controller.GetProjectTasks(id);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void projectController_GetProjectTasks_ReturnsBadRequest()
        {
            int id = 0;
            A.CallTo(() => unitOfWork.Project.Exist(id)).Returns(false);
            var controller = new ProjectsController(unitOfWork, mapper);

            var result = await controller.GetProjectTasks(id);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(BadRequestResult));
        }





        [Fact]
        public async void ProjectsController_Create_ReturnsOk()
        {
            //Arrange
            var projectDto = A.Fake<ProjectDTO>();
            var project = A.Fake<Project>();
            A.CallTo(() => mapper.Map<Project>(projectDto)).Returns(project);
            var controller = new ProjectsController(unitOfWork, mapper);


            //Act
            var result = await controller.Create(projectDto);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

        }
        [Fact]
        public async void ProjectsController_Create_ReturnsBadRequest()
        {
            //Arrange
            var controller = new ProjectsController(unitOfWork, mapper);


            //Act
            var result = await controller.Create(null);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(BadRequestObjectResult));
        }



        [Fact]
        public async void ProjectsController_Delete_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var project = A.Fake<Project>();
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(project);
            var controller = new ProjectsController(unitOfWork, mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ProjectsController_Delete_ReturnsNotFound()
        {
            //Arrange
            int id = 0;
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(null);
            var controller = new ProjectsController(unitOfWork, mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NotFoundResult));
        }



        [Fact]
        public async void ProjectsController_Update_ReturnsCreatedAtAction()
        {
            int id = 1;
            var projectDto = A.Fake<ProjectDTO>();
            projectDto.Id = id;
            var project = A.Fake<Project>();
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(project);
            var controller = new ProjectsController(unitOfWork,mapper);

            var result = await controller.Update(id, projectDto);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async void ProjectsController_Update_WhenNull_ReturnsBadRequestl()
        {
            int id = 1;
            var controller = new ProjectsController(unitOfWork, mapper);

            var result = await controller.Update(id, null);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(BadRequestResult));
        }

        [Fact]
        public async void ProjectsController_Update_WhenDifferentId_ReturnsBadRequest()
        {
            int id = 1;
            var projectDto = A.Fake<ProjectDTO>();
            projectDto.Id = 2;
            var controller = new ProjectsController(unitOfWork, mapper);

            var result = await controller.Update(id, projectDto);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(BadRequestResult));
        }
        [Fact]
        public async void ProjectsController_Update_WhenWrongId_ReturnsBadRequest()
        {
            int id = 1;
            var projectDto = A.Fake<ProjectDTO>();
            projectDto.Id = 1;
            A.CallTo(() => unitOfWork.Project.GetById(id)).Returns(null);
            var controller = new ProjectsController(unitOfWork, mapper);

            var result = await controller.Update(id, projectDto);

            result.Result.Should().NotBeNull();
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}