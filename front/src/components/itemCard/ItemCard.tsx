import { Button, CardActions, CardContent, Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';

export function ItemCard({ image, name, description, showDetail }: any) {
  return (
    <Card sx={{ maxWidth: 320 }}>
      <CardMedia component="img" height="140" image={image} alt={name} />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {description}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={() => showDetail({ image, name, description })}>
          Details
        </Button>
      </CardActions>
    </Card>
  );
}
