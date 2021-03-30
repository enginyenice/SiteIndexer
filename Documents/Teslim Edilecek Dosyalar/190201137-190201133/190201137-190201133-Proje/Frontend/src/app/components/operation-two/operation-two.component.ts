import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { OperationTwoModel } from 'src/app/models/operation-two/operationTwoModel';
import { OperationTwoService } from 'src/app/services/operationTwo/operation-two.service';
import { NzNotificationService } from 'ng-zorro-antd/notification';

@Component({
  selector: 'app-operation-two',
  templateUrl: './operation-two.component.html',
  styleUrls: ['./operation-two.component.css']
})
export class OperationTwoComponent implements OnInit {
  @ViewChild(TemplateRef, { static: false }) template?: TemplateRef<{}>;
  constructor(private operationTwoService:OperationTwoService,private notificationService: NzNotificationService) { }
  dataLoaded = false;
  send = false;
  ngOnInit(): void {
  }

  operationTwoData: OperationTwoModel;
  getKeyword(url:String){
    if(url != "" && url.length != 0 && url != null){
    this.send=true;
    this.operationTwoService.getKeyword(url).subscribe(response => {
      this.operationTwoData = response.data;
      this.dataLoaded=true;  
      this.send=false;
      console.log(response)
    })
  } else {
    this.notificationService.template(this.template!, {
      nzData: {
        title: 'Hata!!',
        message: 'Url hatalı. Tekrar kontrol ediniz.',
        color: 'red',
      },
    });
  }

}
}
