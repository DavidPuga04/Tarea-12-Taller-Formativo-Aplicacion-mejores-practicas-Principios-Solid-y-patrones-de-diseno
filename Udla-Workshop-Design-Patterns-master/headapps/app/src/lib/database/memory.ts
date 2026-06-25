import { Vehicle } from "../types/vehicle";

export class MemoryDatabase {

   vehicles: Vehicle[] = [];
  
    private static _instance: MemoryDatabase;

    constructor() {
      this.vehicles = [];
        
    }

    static get instance() {
      if (!this._instance) {
        this._instance = new MemoryDatabase();
      }
      return this._instance;
    }

  }

  // singleton instance
  export const memoryDatabase = MemoryDatabase.instance;