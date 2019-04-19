﻿using System;
using System.Linq;

using Database;
using FluentValidation;

namespace Web.Modules
{
    public class SimulationModule : BaseModule<Simulation, SimulationRequest, SimulationResponse>
    {
        InlineValidator<Simulation> startValidator = new InlineValidator<Simulation>();

        public SimulationModule()
        {
            header = "simulations";
            Init();

            //addValidator.RuleFor(o => o.Map).Must(BeValidMap).WithMessage("You must specify a valid Map Id");

            //editValidator.RuleFor(o => o.Map).Must(BeValidMap).WithMessage("You must specify a valid Map Id");

            //startValidator.RuleFor(o => o.Map).Must(BeValidMap).WithMessage("You must specify a valid Map Id");
        }

        protected override void Init()
        {
            base.Init();
            Start();
        }

        protected void Start()
        {
            Post("/simulations/{id}/start", x =>
            {
                try
                {
                    int id = x.id;
                    using (var db = DatabaseManager.Open())
                    {
                        var boundObj = db.Single<Simulation>(id);

                        startValidator.ValidateAndThrow(boundObj);
                        BundleManager.instance.Load(new Uri(db.Single<Map>(boundObj.Map).Url).LocalPath);
                        // TODO: initiate download boundObj here if needed
                        // ...
                    }
                    return new
                    {
                        status = "success"
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        status = "error",
                        error = $"Failed to add {typeof(Simulation).ToString()}: {ex.Message}.",
                    };
                }
            });
        }

        protected static bool BeValidMap(int mapId)
        {
            return DatabaseManager.CurrentDb.SingleOrDefault<Map>(mapId) != null;
        }

        protected static bool BeValidVehicles(string vehicleIds)
        {
            string[] ids = vehicleIds.Split(',');

            for (int i = 0; i < ids.Length; i++)
            {
                if (DatabaseManager.CurrentDb.SingleOrDefault<Vehicle>(Convert.ToInt32(ids[i])) == null)
                {
                    return false;
                }
            }

            return true;
        }

        protected override Simulation ConvertToModel(SimulationRequest simRequest)
        {
            Simulation simulation = new Simulation();
            simulation.Name = simRequest.name;
            simulation.Map = simRequest.map;
            if (simRequest.vehicles != null && simRequest.vehicles.Length > 0)
            {
                simulation.Vehicles = string.Join(",", simRequest.vehicles.Select(x => x.ToString()).ToArray());
            }

            simulation.ApiOnly = simRequest.apiOnly;
            simulation.Interactive = simRequest.interactive;
            simulation.OffScreen = simRequest.offScreen;
            simulation.Cluster = simRequest.cluster;
            simulation.TimeOfDay = simRequest.timeOfDay;
            if (simRequest.weather != null)
            {
                simulation.Rain = simRequest.weather.rain;
                simulation.Fog = simRequest.weather.fog;
                simulation.Cloudiness = simRequest.weather.cloudiness;
                simulation.Wetness = simRequest.weather.wetness;
            }

            return simulation;
        }

        protected override SimulationResponse ConvertToResponse(Simulation simulation)
        {
            SimulationResponse simResponse = new SimulationResponse();
            simResponse.Name = simulation.Name;
            simResponse.Map = simulation.Map;
            if (simulation.Vehicles != null && simulation.Vehicles.Length > 0)
            {
                simResponse.Vehicles = simulation.Vehicles.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
            }

            simResponse.ApiOnly = simulation.ApiOnly;
            simResponse.Interactive = simulation.Interactive;
            simResponse.OffScreen = simulation.OffScreen;
            simResponse.Cluster = simulation.Cluster;
            if (simulation.TimeOfDay != null)
            {
                simResponse.TimeOfDay = simulation.TimeOfDay;
            }

            simResponse.Id = simulation.Id;
            Weather w = new Weather();
            w.rain = simulation.Rain;
            w.fog = simulation.Fog;
            w.wetness = simulation.Wetness;
            w.cloudiness = simulation.Cloudiness;
            simResponse.Weather = w;
            return simResponse;
        }
    }
}