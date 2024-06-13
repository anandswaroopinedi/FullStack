import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Location } from '../../Models/location';
import { WebApiUrls } from '../../webapi-urls';
@Injectable({
  providedIn: 'root',
})
export class LocationService {
  constructor(private http: HttpClient,private apiUrls:WebApiUrls) {}
  getLocations(): Observable<Location[]> {
    return this.http.get<Location[]>(this.apiUrls.locations);
  }
}
