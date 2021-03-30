import { OperationThreeModel } from "../operation-three/operationThreeModel";

export interface OperationFourModel extends OperationThreeModel{
    urlTree: UrlTree[]
}

export interface UrlTree{
    key:string,
    title:string,
    url:string,
    children:UrlTree[]
}
