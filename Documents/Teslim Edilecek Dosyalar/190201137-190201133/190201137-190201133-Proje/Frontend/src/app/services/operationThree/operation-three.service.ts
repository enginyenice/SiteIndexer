import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemResponseModel } from 'src/app/models/itemResponseModel';
import { OperationThreeModel } from 'src/app/models/operation-three/operationThreeModel';
import { WebSiteBasic } from 'src/app/models/websiteBasic/WebSiteBasic';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OperationThreeService {

  constructor(private httpClient:HttpClient) { }
  apiUrl = environment.apiUrl+"stagethree";
  
  getSimilarity(webSite:string,webSitePool:WebSiteBasic[]):Observable<ItemResponseModel<OperationThreeModel>> 
  {
    return this.httpClient.post<ItemResponseModel<OperationThreeModel>>(this.apiUrl,{webSite : {url:webSite}, webSitePool : webSitePool});
  }
}
