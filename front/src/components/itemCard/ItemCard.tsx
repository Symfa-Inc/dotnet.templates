import { Button, CardActions, CardContent, Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';

export function ItemCard({ image, title, description }: any) {
  const handleClick = () => {
    console.log(`Clicked on ${title}`);
  };

  return (
    <Card sx={{ maxWidth: 320 }}>
      <CardMedia component="img" height="140" image={image} alt={title} />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {title}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {description}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={handleClick}>
          Details
        </Button>
      </CardActions>
    </Card>
  );
}
