import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Product } from "../../models/product";

export interface BasketState {
  products: Product[];
}

export const initialState: BasketState = {
  products: []
};

export const basketSlice = createSlice({
  name: "basket",
  initialState,
  reducers: {
    addProduct: (state, action: PayloadAction<Product>) => {
      state.products.push(action.payload);
    },        
  },
});

export const { addProduct } = basketSlice.actions;
export default basketSlice.reducer;