import { ProductSavings } from "./product-savings";
import { Promotion } from "./promotion";

export interface TotalSummary {
    totalBeforePromotions: number;    
    finalTotal: number;
    promotionSavings: ProductSavings[];
}