import { Card, Empty, Spin, Button, notification } from "antd";
import Meta from "antd/lib/card/Meta";
import React from "react";
import { useQuery } from "react-query";
import { useDispatch } from "react-redux";
import { Product } from "../../models/product";
import { getProducts } from "../../services/shop-service";
import { actions } from "../index";
import styles from "./shop-list.module.scss";

export const ShopList = () => {
    const { isLoading, data } = useQuery(["getProducts"], () => getProducts());

    const dispatch = useDispatch();

    const handleAddToBasket = (product: Product) => {
        dispatch(actions.addProduct(product));
        notification.success({message: "Product added to the basket", placement: "topLeft"});
    };
    
    return (
        <>
            {isLoading && <Spin />}

            {!data && !isLoading && <Empty />}

            <div className={styles.container}>
                {!isLoading && data &&
                    data?.map((product) => (
                        <Card
                            key={product.id}
                            hoverable
                            className={styles.card}                            
                        >
                            <Meta title={product.name} description={product.description} />  
                            <div className={styles.price}>
                                {`£ ${product.price}`}
                            </div>                                                                                  
                            <div className={styles.addToBasket} onClick={() => handleAddToBasket(product)}>
                                <Button type="primary">Add to basket</Button>
                            </div>                         
                        </Card>
                    ))
                }
            </div>
        </>
    )
}