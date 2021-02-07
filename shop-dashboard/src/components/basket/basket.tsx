import { Badge, Popover, Spin } from 'antd';
import { ShoppingCartOutlined } from "@ant-design/icons";
import styles from "./basket.module.scss"
import { useSelector } from 'react-redux';
import { SystemState } from '../../core/root-reducer';
import { useQuery } from 'react-query';
import { getTotal } from '../../services/shop-service';

export const Basket = () => {
    const { products } = useSelector((state: SystemState) => state.basket);
    const { isLoading, data } = useQuery(["getTotal", products], () => getTotal(products), {
        enabled: products !== null && products.length > 0
    });
        
    const content = (
        <>
        {isLoading && <Spin />}
        
        {!isLoading && data &&
            <>
                <div className={styles.selectedProducts}>Selected products</div>
                <ul>
                   {products.map((product) => (
                    <li key={product.id}>
                        {product.name}                         
                    </li>
                   ))} 
                </ul>
                <div className={styles.content}>
                    <p>Without savings: £ {data.totalBeforePromotions}</p>
                    <p>Savings</p>
                    {data.promotionSavings && 
                    <div>
                        {data.promotionSavings.map((promotion) => (
                            <>
                                <p>{promotion.promotion.name}</p>
                                <p>You saved (£{promotion.savings})</p>
                            </>
                        ))}
                    </div>
                    }
                    <p>Total to pay: £ {data.finalTotal}</p>            
                </div>
            </>
        }
        </>
      );

    const handlePopoverVisibleChange = (visible: boolean) => {
        if (visible) {

        }
    };
    
    return (
        <Popover content={content} title="Basket Summary" onVisibleChange={handlePopoverVisibleChange}>
            <div className={styles.basket}>
                <ShoppingCartOutlined />
                <Badge count={products.length} />
            </div>
        </Popover>
    )
};