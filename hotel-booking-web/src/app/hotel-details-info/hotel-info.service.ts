import { HotelInfo } from './hotel-info.model';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HotelInfoService {
  private readonly URL = 'https://localhost:7166/api/HotelDetails';

  constructor(private http: HttpClient) { }

  getAllHotelInfo(): Observable<HotelInfo[]> {
    return this.http.get<HotelInfo[]>(this.URL).pipe(
      tap(data => console.log(JSON.stringify(data)))
    );
  }

  getHotelInfoById(id: number): Observable<HotelInfo> {
    return this.http.get<HotelInfo>(`${this.URL}/${id}`).pipe(
      tap(data => console.log('Hotel Info Data: ', JSON.stringify(data)))
    );
  }

  addHotelInfo(hotelInfo: HotelInfo): Observable<HotelInfo> {
    return this.http.post<HotelInfo>(`${this.URL}`, hotelInfo);
  }

  updateHotelInfo(hotelInfo: HotelInfo): Observable<HotelInfo> {
    return this.http.put<HotelInfo>(`${this.URL}/${hotelInfo.id}`, hotelInfo);
  }

  uploadPictures(files: any, hotelName: string): Observable<any[]> {
    return this.http.post<any[]>(`${this.URL}/upload?hotelName=${hotelName}`, files);
  }
}
