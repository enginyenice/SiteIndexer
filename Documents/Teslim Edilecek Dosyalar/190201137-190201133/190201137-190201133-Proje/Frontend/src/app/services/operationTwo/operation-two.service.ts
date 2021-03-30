import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemResponseModel } from 'src/app/models/itemResponseModel';
import { OperationTwoModel } from 'src/app/models/operation-two/operationTwoModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OperationTwoService {

  constructor(private httpClient:HttpClient) { }
  apiUrl = environment.apiUrl+"stagetwo";
    getKeyword(Url:String):Observable<ItemResponseModel<OperationTwoModel>> {
    return this.httpClient.post<ItemResponseModel<OperationTwoModel>>(this.apiUrl, { url: Url });

    
  }
}
