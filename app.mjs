// app.mjs

import express from 'express';


const app = express();
const port = 3000;

// Serve static files from the public directory
app.use('/public', express.static('public'));

// Define a simple array of products


// Set the view engine to EJS
app.set('view engine', 'ejs');

// Define a route for the homepage
app.get('/', (req, res) => {
  res.render('pages/home', { title: "Home Page"});
});
app.get('/account', (req, res)=> {
  res.render('pages/account', {title: "Account Page"});
});
app.get('/signup', (req,res )=> {
  res.render('pages/signup', {title: "SignUp Page"});
});
app.get('/search', (req, res) => {
  res.render('pages/search', {title: "Search page"});
});
app.get('/admin', (req, res)=> {
  res.render('pages/admin', {title: "Admin page"});
});
app.get('/admin/add-product', (req, res)=> {
  res.render('pages/add-product', {title: "Add product page"});
});
app.get('/product/:productId', (req, res) => {
  const productId = req.params.productId;
  res.render('pages/product', { title: "Product Page", productId: productId });
});

// Start the server
app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});
