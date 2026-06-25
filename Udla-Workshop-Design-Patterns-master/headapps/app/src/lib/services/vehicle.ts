import { MemoryDatabase } from "../database/memory";
import { Vehicle } from "../types/vehicle";

export class VehicleService {
  private memoryDb = MemoryDatabase.instance;

  all(): Vehicle[]{
    return this.memoryDb.vehicles;
  }

  add(vehicle: Vehicle): void {
    this.memoryDb.vehicles.push(vehicle); 
  }
  
}