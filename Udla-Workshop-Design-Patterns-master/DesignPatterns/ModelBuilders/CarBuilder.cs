using DesignPatterns.Models;
using System;

namespace DesignPatterns.ModelBuilders
{
    // Builder encargado de construir vehículos con propiedades por defecto.
    public class CarBuilder
    {
        private string _color = "Red";
        private string _brand = "Ford";
        private string _model = "Mustang";

        public CarBuilder SetColor(string color)
        {
            _color = color;
            return this;
        }

        public CarBuilder SetBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        public CarBuilder SetModel(string model)
        {
            _model = model;
            return this;
        }

        public Car Build()
        {
            var car = new Car(
                _color,
                _brand,
                _model
            );

            // Valor por defecto solicitado
            car.Year = DateTime.Now.Year;

            return car;
        }
    }
}