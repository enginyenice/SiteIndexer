import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemResponseModel } from 'src/app/models/itemResponseModel';
import { OperationFiveModel } from 'src/app/models/operation-five/operationFiveModel';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OperationFiveService {

  constructor(private httpClient:HttpClient) { }
  apiUrl = environment.apiUrl+"stagefive";
  
  getSimilarity(webSite:string,webSitePool:WebSiteBasic[]):Observable<ItemResponseModel<OperationFiveModel>> 
  {
    return this.httpClient.post<ItemResponseModel<OperationFiveModel>>(this.apiUrl,{webSite : {url:webSite}, webSitePool : webSitePool});
  }
}
