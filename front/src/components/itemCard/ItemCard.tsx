import { Button, CardActions, CardContent, Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardMedia from '@mui/material/CardMedia';
import { COLORS } from '@styles/colorPalette';

export function ItemCard({ name, description, image, showDetail }: any) {
  return (
    <Card sx={{ maxWidth: 320, margin: 'auto' }}>
      <CardMedia component="img" height="140" image={image || 'https://source.unsplash.com/random'} alt={name} />
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {description}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" sx={{ color: COLORS.link }} onClick={() => showDetail({ image, name, description })}>
          Details
        </Button>
      </CardActions>
    </Card>
  );
}
