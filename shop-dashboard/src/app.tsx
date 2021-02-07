import styles from './app.module.scss';
import { Layout } from 'antd';
import { ShopList } from './components/shop-list/shop-list';
import {
  BrowserRouter as Router,
  Switch,
  Route,  
} from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./core/store";
import { Basket } from "./components/basket/basket";

const { Header, Content, Footer } = Layout;

export const App = () => {  
  return (
    <Router>
      <Provider store={store}>
      <Layout className={styles.container}>
        <Header className={styles.header}>
          <div className={styles.headerContainer}>
            <div>
              Welcome to the Good Energy Store
            </div>
            <Basket />
          </div>          
        </Header>
        <Content className={styles.content}>
          <Switch>            
            <Route path="/">
              <ShopList />
            </Route>
          </Switch>          
        </Content>
        <Footer className={styles.footer}>Good Energy!</Footer>      
      </Layout>
      </Provider>
    </Router>
  );
}

export default App;
