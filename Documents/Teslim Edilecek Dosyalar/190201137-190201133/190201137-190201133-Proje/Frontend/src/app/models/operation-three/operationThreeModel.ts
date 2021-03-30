import { Keyword } from "../keyword/keywordModel";
import { SemanticKeyword } from "../semanticKeyword/semanticKeyword";

//Website modeli
export interface Website{
    title:string;
    url:string;
    keywords:Keyword[];
    semanticKeywords:SemanticKeyword[]
}

// Sonuc Modelindeki Benzerligi hesaplanan websiteler ve benzerlik skorlari modeli
interface SimularityScore{
    similarityScore:number;
    webSite:Website;
}

// Sonuc Modeli
export interface OperationThreeModel{
    webSite:Website;
    webSitePool:SimularityScore[];
}
