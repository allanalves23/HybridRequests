# HybridRequests

![Build](https://github.com/allanalves23/HybridRequests/workflows/.NET/badge.svg)
[![Nuget](https://img.shields.io/nuget/v/HybridRequests?style=flat-square)](https://www.nuget.org/packages/HybridRequests/)

> Disclaimer: This package is not finished for production use, use it at your own risk.

# HybridRequests

## Motivation

This project aims to deserialize your order information into a single object. On ordinary days, join commands are required to call their respective handlers, with `put` /` patch` / `get` requests 2 or more information can be passed at the same time. Therefore, it is necessary to do:

```
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpPut("{Id}")]
        public IActionResult UpdateCar(UpdateCarCommand car)
        {
            // Call to your handle
            return NoContent();
        }
    }
```

Respective Command:

```
    public class UpdateCarCommand
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
    }
```

Observe that `car` may be passed originally from Body, same that ignoring `[FromBody]` Annotation. To Get `id` property from `route` must necessary explicit other parameter from Action Controller:

```
        [HttpPut("{Id}")]
        public IActionResult UpdateCar(UpdateCarCommand car, int id)
        {
            car.Id = id; // Bad
            // Call to your handle
            return NoContent();
        }
```

Thus, this library is motivated to add this information to only one Class, Command or any model that you can deserialize.

```
        [HttpPut("{id}")]
        public IActionResult Post([ModelBinder(typeof(HybridRequest))]CarModel car)
        {
            // `car` property should retain route `id` value.
            // Call to your handle
            return NoContent();
        }
```


## Installing

This project is supported only `.NET 5`, to install with dotnet CLI just:

`dotnet add package HybridRequests --version 0.0.4-dev`

or with Package Manager 

`Install-Package HybridRequests -Version 0.0.4-dev`

___

## Development

Pull Requests or Issues are welcome. If want contribute just create a fork this project and request a PR.

### Testing

Just run the following command on solution directory:

`dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info  tests/HybridRequests.Unit.Tests -v n`
