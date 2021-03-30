import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemResponseModel } from 'src/app/models/itemResponseModel';
import { OperationOneModel } from 'src/app/models/operation-one/operationOneModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OperationOneService {

  constructor(private httpClient: HttpClient) { }

  apiUrl = environment.apiUrl + 'stageone';
  getFrequency(Url: String):Observable<ItemResponseModel<OperationOneModel>> {
    return this.httpClient.post<ItemResponseModel<OperationOneModel>>(this.apiUrl, { url: Url });
  }
}
