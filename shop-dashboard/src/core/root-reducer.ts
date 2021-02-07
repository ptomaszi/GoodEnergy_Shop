import { combineReducers } from "redux";

import { BasketReducer } from "../components/index";

export const rootReducer = combineReducers({
  basket: BasketReducer,  
});

export type SystemState = ReturnType<typeof rootReducer>;
