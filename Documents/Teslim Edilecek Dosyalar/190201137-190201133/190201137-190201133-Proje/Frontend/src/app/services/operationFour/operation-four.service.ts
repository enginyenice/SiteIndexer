import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemResponseModel } from 'src/app/models/itemResponseModel';
import { OperationFourModel } from 'src/app/models/operation-four/operationFourModel';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OperationFourService {

  constructor(private httpClient:HttpClient) { }
  apiUrl = environment.apiUrl+"stagefour";
  
  getSimilarity(webSite:string,webSitePool:WebSiteBasic[]):Observable<ItemResponseModel<OperationFourModel>> 
  {
    return this.httpClient.post<ItemResponseModel<OperationFourModel>>(this.apiUrl,{webSite : {url:webSite}, webSitePool : webSitePool});
  }
}
